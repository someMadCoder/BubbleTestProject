using Code.Infrastructure.Services;
using Code.Infrastructure.States;

namespace Code.Infrastructure
{
    public class Game
    {
        public GameStateMachine _stateMachine;

        public Game(ICoroutineRunner coroutineRunner)
        {
            _stateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), AllServices.Container);
        }
    }
}