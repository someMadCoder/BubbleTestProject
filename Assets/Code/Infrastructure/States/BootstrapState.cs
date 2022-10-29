using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Factory;
using Code.Infrastructure.Services;
using Code.Services.Input;

namespace Code.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private const string MainMenu = "MainMenu";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services =  services;
            RegisterServices();
        }
        public void Enter()
        {
            _sceneLoader.Load(Initial, EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>(MainMenu);
        }

        public void Exit()
        {
        
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(new TouchInputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(
                new GameFactory(_services.Single<IAssetProvider>()));
        }


    }
}