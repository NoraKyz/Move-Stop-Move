
namespace _Pattern.StateMachine
{
    public interface IState<in T>
    {
        public void OnEnter(T t);
        public void OnExecute(T t);
        public void OnExit(T t);
    }
}
