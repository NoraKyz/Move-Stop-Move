using _Game.Scripts.Other.Utils;
using _SDK.StateMachine;
using _SDK.UI.Base;

namespace _Game.Scripts.GamePlay.Character.State.PlayerState
{
    public class PlayerIdleState : IState<Player.Player>
    {
        public void OnEnter(Player.Player player)
        {
            player.ChangeAnim(AnimName.IDLE);
        }

        public void OnExecute(Player.Player player)
        {
            if (GameManager.IsState(GameState.GamePlay) == false)
            {
                return;
            }
                
            
            if (player.IsMoving)
            {
                player.ChangeState(new PlayerRunState());
            }

            if (player.HasEnemyInRange && player.IsAttackAble)
            {
                player.ChangeState(new PlayerAttackState());
            }
        }

        public void OnExit(Player.Player player)
        {
            
        }
    }
}