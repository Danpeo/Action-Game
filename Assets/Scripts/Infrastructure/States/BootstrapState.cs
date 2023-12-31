using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.SaveLoad;
using Infrastructure.Services;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.Ranomizer;
using UI.Services.Factory;
using UI.Services.Windows;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialSceneName = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(InitialSceneName, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            RegisterStaticData();

            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            
            _services.RegisterSingle(InputService());

            _services.RegisterSingle<IRandomService>(new RandomService());

            RegisterAssetProvider();

            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());

            _services.RegisterSingle<IUiFactory>(new UiFactory(_services.Single<IAssets>(),
                _services.Single<IStaticDataService>(),
                _services.Single<IPersistentProgressService>()
            ));

            _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUiFactory>()));

            _services.RegisterSingle<IGameFactory>(
                new GameFactory(_services.Single<IAssets>(),
                    _services.Single<IStaticDataService>(),
                    _services.Single<IRandomService>(),
                    _services.Single<IPersistentProgressService>(),
                    _services.Single<IWindowService>()
                ));

            _services.RegisterSingle<ISaveLoadService>(
                new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
        }

        private void RegisterAssetProvider()
        {
            var assetProvider = new AssetProvider();
            _services.RegisterSingle<IAssets>(assetProvider);
            assetProvider.Initialize();
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadEnemies();
            _services.RegisterSingle(staticDataService);
        }

        private IInputService InputService()
        {
            /*if (Application.isEditor)
                return new StandaloneInputService();
            else*/
            return new StandaloneInputService();
        }
    }
}