using Code.Infrastructure.States;
using UnityEngine;

namespace Code.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;

        private void Awake()
        {
            _game = new Game(this);
            _game._stateMachine.Enter<BootstrapState>();
            DontDestroyOnLoad(this);
        }
    }
}
