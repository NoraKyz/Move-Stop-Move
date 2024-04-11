using _Game.Scripts.Other.Utils;
using _SDK.StateMachine;
using _SDK.UI.Base;

namespace _Game.Scripts.GamePlay.Character.State.PlayerState
{
    public class PlayerRunState : IState<Player.Player>
    {
        public void OnEnter(Player.Player player)
        {
            player.ChangeAnim(AnimName.RUN);
        }

        public void OnExecute(Player.Player player)
        {
            if (player.IsMoving == false || GameManager.IsState(GameState.GamePlay) == false)
            {
                player.ChangeState(new PlayerIdleState());
            }
            
            player.Move();
        }

        public void OnExit(Player.Player player)
        {
            player.StopMove();
        }
    }
}