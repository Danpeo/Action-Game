using Infrastructure.Services;

namespace Infrastructure.States
{
    public interface IGameStateMachine : IService
    {
        void Enter<TypeState>() where TypeState : class, IState;
        void Enter<TypeState, TPayLoad>(TPayLoad payLoad) where TypeState : class, IPayLoadState<TPayLoad>;
    }
}