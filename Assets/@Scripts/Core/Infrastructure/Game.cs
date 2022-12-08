using Core.Infrastructure.Services;
using Core.Infrastructure.States;
using Core.Services;

namespace Core.Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner)
        {
            StateMachine = new GameStateMachine(ServiceLocator.Container);
        }
    }
}