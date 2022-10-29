using Code.Infrastructure.Factory;

namespace Code.Infrastructure.States
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _stateMachine;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
        }
        
        public void Exit()
        {
            
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        private void OnLoaded()
        {
            _gameFactory.CreateHUD();
            _gameFactory.CreateBackground();
            _stateMachine.Enter<GameLoopState, Level>(null);
        }

        
    }
}