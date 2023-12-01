using _Game.Scripts.Character.Player;
using _Game.Scripts.Utils;

namespace _Pattern.StateMachine.PlayerState
{
    public class PlayerRunState : IState<Player>
    {
        public void OnEnter(Player player)
        {
            player.ChangeAnim(AnimName.Run);
        }

        public void OnExecute(Player player)
        {
            player.Move();

            if (player.IsMoving == false)
            {
                player.ChangeState(new PlayerIdleState());
            }
        }

        public void OnExit(Player player)
        {
            
        }
    }
}