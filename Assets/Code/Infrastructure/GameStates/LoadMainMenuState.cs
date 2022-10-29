using UnityEngine;
using UnityEngine.UI;

namespace Code.Infrastructure.GameStates
{
    public class LoadMainMenuState : IPayLoadedState<string>
    {
        private const string GameScene = "Game";
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _stateMachine;

        public LoadMainMenuState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
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
            ConfigurePlayButton();
        }

        private void ConfigurePlayButton()
        {
            PlayButton playButton = GameObject.FindObjectOfType<PlayButton>();
            if (playButton == null) Debug.LogError("Play button does`t exist");
            if (playButton.TryGetComponent(out Button button))
            {
                button.onClick.AddListener(OnPlayButtonClick);
            }
        }

        private void OnPlayButtonClick()
        {
            _stateMachine.Enter<LoadLevelState, string>(GameScene);
        }
    }
}