using System;
using System.Collections.Generic;
using GTN.Utilities;
using JetBrains.Annotations;
using Zenject;

namespace GTN.Base.FSM
{
    [UsedImplicitly]
    public class StateMachine : IStateMachine, IInitializable
    {
        private IStateBase _currentState;
        private readonly DiContainer _container;
        private readonly Dictionary<Type, IStateBase> _stateRegistry;

        public StateMachine(DiContainer diContainer)
        {
            _container = diContainer;
            _stateRegistry = new Dictionary<Type, IStateBase>();
        } 
        
        public void Initialize()
        {
            var states = _container.ResolveAll<IStateBase>();
            Register(states);
            this.GoBootstrap();
        }

        private void Register(IEnumerable<IStateBase> states)
        {
            foreach (var state in states)
            {
                _stateRegistry.Add(state.GetType(), state);
            }
        }

        public void EnterState<TState>()
            where TState : class, IGameState
        {
            _currentState?.Exit();
            var newSate = FindState<TState>();
            _currentState = newSate;
            newSate.Enter();
        }

        public void EnterState<TState, TPayload>(TPayload payload)
            where TState : class, IGameState<TPayload>
            where TPayload : struct
        {
            _currentState?.Exit();
            var newSate = FindState<TState>();
            _currentState = newSate;
            newSate.Enter(payload);
        }
        
        private TState FindState<TState>() where TState : class, IStateBase
        {
            if (_stateRegistry.TryGetValue(typeof(TState), out var state))
                return state as TState;
            throw new Exception($"State {typeof(TState).Name} not found.");
        }
    }
}
