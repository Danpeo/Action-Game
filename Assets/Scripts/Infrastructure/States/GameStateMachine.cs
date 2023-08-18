using System;
using System.Collections.Generic;
using Infrastructure.Factory;
using Infrastructure.SaveLoad;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using UI;
using UI.Elements;
using UI.Services.Factory;

namespace Infrastructure.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain, AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),

                [typeof(LoadSceneState)] =
                    new LoadSceneState(this, sceneLoader, curtain, services.Single<IGameFactory>(),
                        services.Single<IPersistentProgressService>(),
                        services.Single<IStaticDataService>(), 
                        services.Single<IUiFactory>()),

                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentProgressService>(),
                    services.Single<ISaveLoadService>()),

                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }

        public void Enter<TypeState>() where TypeState : class, IState
        {
            IState state = ChangeState<TypeState>();
            state.Enter();
        }

        public void Enter<TypeState, TPayLoad>(TPayLoad payLoad) where TypeState : class, IPayLoadState<TPayLoad>
        {
            TypeState state = ChangeState<TypeState>();
            state.Enter(payLoad);
        }

        private TypeState ChangeState<TypeState>() where TypeState : class, IExitableState
        {
            _activeState?.Exit();
            TypeState state = GetState<TypeState>();
            _activeState = state;

            return state;
        }

        private TypeState GetState<TypeState>() where TypeState : class, IExitableState =>
            _states[typeof(TypeState)] as TypeState;
    }
}