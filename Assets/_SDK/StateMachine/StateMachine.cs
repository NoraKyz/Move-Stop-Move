﻿namespace _SDK.StateMachine
{
    public class StateMachine<T>
    {
        private IState<T> _currentState;
        private T _owner;

        public StateMachine(T owner)
        {
            _owner = owner;
        }

        public void ChangeState(IState<T> state)
        {
            _currentState?.OnExit(_owner);
            _currentState = state;
            _currentState?.OnEnter(_owner);
        }

        public void UpdateState(T owner)
        {
                _currentState?.OnExecute(owner);
        }
    }
}