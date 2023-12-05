using _Game.Scripts.Character.Player;
using _Game.Scripts.Utils;
using UnityEngine;

namespace _Pattern.StateMachine.PlayerState
{
    public class PlayerAttackState : IState<Player>
    {
        private float _timer;
        private const float AttackDelay  = 0.5f;
        public void OnEnter(Player player)
        {
            _timer = 0;
        }

        public void OnExecute(Player player)
        {
            if (player.IsMoving)
            {
                player.ChangeState(new PlayerRunState());
            }
            
            // _timer += Time.deltaTime;
            //
            // if (_timer >= AttackDelay)
            // {
            //     player.ChangeState(new PlayerIdleState());
            // }
        }

        public void OnExit(Player player)
        {
            
        }
    }
}