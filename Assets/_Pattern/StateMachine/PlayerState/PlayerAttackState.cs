using _Game.Scripts.Character.Player;
using _Game.Scripts.Utils;
using UnityEngine;

namespace _Pattern.StateMachine.PlayerState
{
    public class PlayerAttackState : IState<Player>
    {
        private const float AttackTime = 1f;
        private float _timer;
        public void OnEnter(Player player)
        {
            _timer = 0;
            
            player.ChangeAnim(AnimName.Attack);
            player.Attack();
        }

        public void OnExecute(Player player)
        {
            if (player.IsMoving)
            {
                player.ChangeState(new PlayerRunState());
            }

            _timer += Time.deltaTime;
            if (_timer >= AttackTime)
            {
                player.ChangeState(new PlayerIdleState());
            }
        }

        public void OnExit(Player player)
        {
            
        }
    }
}