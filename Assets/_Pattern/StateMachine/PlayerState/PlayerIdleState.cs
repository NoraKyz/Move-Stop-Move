using _Game.Scripts.Character.Player;
using _Game.Scripts.Utils;

namespace _Pattern.StateMachine.PlayerState
{
    public class PlayerIdleState : IState<Player>
    {
        public void OnEnter(Player player)
        {
            player.ChangeAnim(AnimName.Idle);
        }

        public void OnExecute(Player player)
        {
            if (player.IsMoving)
            {
                player.ChangeState(new PlayerRunState());
            }
            
            if (player.HasEnemyInRange && player.AttackAble)
            {
                player.ChangeState(new PlayerAttackState());
            }
        }

        public void OnExit(Player player)
        {
            
        }
    }
}