using _Game.Scripts.Character;

namespace _Pattern.StateMachine
{
    public interface IState<in T> where T : Character
    {
        public void OnEnter(T t);
        public void OnExecute(T t);
        public void OnExit(T t);
    }
}
