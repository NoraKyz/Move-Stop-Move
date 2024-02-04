
namespace _Pattern.StateMachine
{
    public class StateMachine<T>
    {
        private IState<T> _currentState;
        private T _owner;

        public void OnInit(T owner)
        {
            _owner = owner;
        }

        public void ChangeState<TState>(TState state) where TState : IState<T>
        {
            if (_currentState != null)
            {
                _currentState.OnExit(_owner);
            }

            _currentState = state;

            if (_currentState != null)
            {
                _currentState.OnEnter(_owner);
            }
        }

        public void UpdateState(T owner)
        {
            if (_currentState != null)
            {
                _currentState.OnExecute(owner);
            }
        }
    }
}