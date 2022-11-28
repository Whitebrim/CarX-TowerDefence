using UnityEngine;

namespace Core.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;

        private void Awake()
        {
            DontDestroyOnLoad(this);

            _game = new Game(this);
            _game.StateMachine.Enter<BootstrapState>();
        }
    }
}