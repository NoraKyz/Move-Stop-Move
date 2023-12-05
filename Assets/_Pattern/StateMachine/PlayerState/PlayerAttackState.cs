using _Game.Scripts.Character.Player;
using _Game.Scripts.Utils;
using UnityEngine;

namespace _Pattern.StateMachine.PlayerState
{
    public class PlayerAttackState : IState<Player>
    {
        public void OnEnter(Player player)
        {
            player.ChangeAnim(AnimName.Attack);
            player.Attack();
        }

        public void OnExecute(Player player)
        {
            if (player.IsMoving)
            {
                player.ChangeState(new PlayerRunState());
            }
        }

        public void OnExit(Player player)
        {
            
        }
    }
}