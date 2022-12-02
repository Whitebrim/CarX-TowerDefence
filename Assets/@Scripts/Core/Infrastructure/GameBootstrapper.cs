using Core.Infrastructure.Services;
using Core.Infrastructure.States;
using Core.Services;
using UnityEngine;

namespace Core.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            DI.Container.RegisterSingle<ICoroutineRunner>(this);

            _game = new Game(this);
            _game.StateMachine.Enter<BootstrapState>();
        }
    }
}