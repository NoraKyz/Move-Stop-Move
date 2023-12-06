using _Game.Scripts.Character.Player;
using _Game.Scripts.Utils;
using UnityEngine;

namespace _Pattern.StateMachine.PlayerState
{
    public class PlayerAttackState : IState<Player>
    {
        private const float AttackTime = 1f;
        private const float AttackSpeed = 0.4f;
        
        private float _timer;
        private Vector3 _targetPos;
        public void OnEnter(Player player)
        {
            _timer = 0;
            _targetPos = player.GetRandomEnemyPos();
            
            player.LookAt(_targetPos);
            player.ChangeAnim(AnimName.Attack);
        }

        public void OnExecute(Player player)
        {
            if (player.IsMoving)
            {
                player.ChangeState(new PlayerRunState());
            }

            _timer += Time.deltaTime;

            if (_timer >= AttackSpeed && player.AttackAble)
            {
                player.Attack(_targetPos);
            } 
            else if (_timer >= AttackTime)
            {
                player.ChangeState(new PlayerIdleState());
            }
        }

        public void OnExit(Player player)
        {
            
        }
    }
}