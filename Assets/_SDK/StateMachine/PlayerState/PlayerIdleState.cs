using _Game.Scripts.GamePlay.Character.Player;
using _Game.Scripts.Other.Utils;
using _Game.Scripts.UI.Base;

namespace _SDK.StateMachine.PlayerState
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

            if (GameManager.IsState(GameState.GamePlay))
            {
                if (player.HasEnemyInRange && player.IsAttackAble)
                {
                    player.ChangeState(new PlayerAttackState());
                }
            }
        }

        public void OnExit(Player player)
        {
            
        }
    }
}