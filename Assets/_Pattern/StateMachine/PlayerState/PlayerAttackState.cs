using _Game.Scripts.GamePlay.Character.Base;
using _Game.Scripts.GamePlay.Character.Player;
using _Game.Scripts.Other.Utils;
using UnityEngine;

namespace _Pattern.StateMachine.PlayerState
{
    public class PlayerAttackState : IState<Player>
    {
        private const float AttackTime = 1f;
        private const float AttackSpeed = 0.4f;
        
        private float _timer;
        private Character _target;
        
        public void OnEnter(Player player)
        {
            _timer = 0;
            _target = player.GetEnemy();
            _target.SetCircleTargetIndicator(true);
            
            player.LookAtTarget(_target.TF.position);
            player.ChangeAnim(AnimName.Attack);
        }

        public void OnExecute(Player player)
        {
            if (player.IsMoving)
            {
                player.ChangeState(new PlayerRunState());
            }

            _timer += Time.deltaTime;

            if (_timer >= AttackSpeed && player.IsAttackAble)
            {
                player.Attack(_target.TF.position);
            } 
            else if (_timer >= AttackTime)
            {
                player.ChangeState(new PlayerIdleState());
            }
        }

        public void OnExit(Player player)
        {
            _target.SetCircleTargetIndicator(false);
        }
    }
}