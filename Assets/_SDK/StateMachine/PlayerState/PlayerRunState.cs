using _Game.Scripts.GamePlay.Character.Player;
using _Game.Scripts.Other.Utils;
using _SDK.UI.Base;
using UnityEngine;

namespace _SDK.StateMachine.PlayerState
{
    public class PlayerRunState : IState<Player>
    {
        public void OnEnter(Player player)
        {
            player.ChangeAnim(AnimName.Run);
        }

        public void OnExecute(Player player)
        {
            if (player.IsMoving == false || GameManager.IsState(GameState.GamePlay) == false)
            {
                player.ChangeState(new PlayerIdleState());
            }
            
            player.Move();
        }

        public void OnExit(Player player)
        {
            player.StopMove();
        }
    }
}