using Core.Infrastructure.Services;
using Core.Services;
using System;
using System.Collections.Generic;

namespace Core.Infrastructure.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IBaseState> _states;
        private IBaseState _currentState;

        public GameStateMachine(DI services)
        {
            _states = new Dictionary<Type, IBaseState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, services),
                [typeof(LoadLevelState)] = new LoadLevelState(this, services.Single<ISceneLoader>()),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState GetState<TState>() where TState : class, IBaseState =>
            _states[typeof(TState)] as TState;

        private TState ChangeState<TState>() where TState : class, IBaseState
        {
            _currentState?.Exit();

            TState state = GetState<TState>();
            _currentState = state;

            return state;
        }
    }
}