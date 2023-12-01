using _Framework.Pool.Scripts;
using _Game.Scripts.Character;

namespace _Pattern.StateMachine
{
    public class StateMachine<T> where T : GameUnit
    {
        private IState<T> _currentState;
        private T _gameUnit;

        public void ChangeState<TState>(TState state) where TState : IState<T>
        {
            if (_currentState != null)
            {
                _currentState.OnExit(_gameUnit);
            }

            _currentState = state;

            if (_currentState != null)
            {
                _currentState.OnEnter(_gameUnit);
            }
        }

        public void UpdateState(T owner)
        {
            if (_currentState != null)
            {
                _currentState.OnExecute(owner);
            }
        }

        public void SetOwner(T owner)
        {
            _gameUnit = owner;
        }
    }
}