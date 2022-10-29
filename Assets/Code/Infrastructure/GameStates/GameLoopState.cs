using Code.Infrastructure.Factory;

namespace Code.Infrastructure.GameStates
{
    public class GameLoopState : IPayLoadedState<Level>
    {
        private IGameFactory _gameFactory;

        public GameLoopState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void Enter(Level level)
        {
        }

        public void Exit()
        {
        }
    }
}