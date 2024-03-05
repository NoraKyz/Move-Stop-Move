
namespace _SDK.StateMachine
{
    public interface IState<in T>
    {
        public void OnEnter(T player);
        
        public void OnExecute(T t);
        
        public void OnExit(T t);
    }
}
