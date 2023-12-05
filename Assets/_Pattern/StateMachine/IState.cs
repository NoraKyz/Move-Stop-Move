using _Framework.Pool.Scripts;

namespace _Pattern.StateMachine
{
    public interface IState<in T> where T : GameUnit
    {
        public void OnEnter(T t);
        public void OnExecute(T t);
        public void OnExit(T t);
    }
}
