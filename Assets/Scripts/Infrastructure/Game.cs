using Infrastructure.Services;
using Infrastructure.States;
using UI;
using UI.Elements;

namespace Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine { get; set; }
        
        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container);
        }
    }
}
