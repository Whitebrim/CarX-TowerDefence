using Core.Infrastructure.Services;
using Core.Infrastructure.States;

namespace Core.Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner)
        {
            StateMachine = new GameStateMachine(DI.Container);
        }
    }
}