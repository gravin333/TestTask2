using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.States
{
    public class GameStateMachine : IService
    {
        private readonly Dictionary<Type, IBaseState> _states;
        private IExitableState _activeState;

        public GameStateMachine(AllServices allServices, SceneLoader sceneLoader)
        {
            _states = new Dictionary<Type, IBaseState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, allServices, sceneLoader),
                [typeof(LoadMenuState)] = new LoadMenuState(this, sceneLoader, allServices.Single<IUIFactory>()),
                [typeof(LoadGameState)] = new LoadGameState(this, sceneLoader)
            };
        }

        public void Enter<TState>() where TState : class, IBaseState
        {
            var changeState = ChangeState<TState>();
            changeState.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IBaseState
        {
            _activeState?.Exit();
            var state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IBaseState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}