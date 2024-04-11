using _Game.Scripts.Other.Utils;
using _SDK.StateMachine;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Character.State.PlayerState
{
    public class PlayerAttackState : IState<Player.Player>
    {
        private const float ATTACK_TIME = 1f;
        private const float ATTACK_SPEED = 0.4f;
        
        private float _timer;
        private Base.Character _target;
        
        public void OnEnter(Player.Player player)
        {
            _timer = 0;
            _target = player.GetEnemy();
            _target.SetCircleTargetIndicator(true);
            
            player.LookAtTarget(_target.TF.position);
            player.ChangeAnim(AnimName.ATTACK);
        }

        public void OnExecute(Player.Player player)
        {
            if (player.IsMoving)
            {
                player.ChangeState(new PlayerRunState());
            }

            _timer += Time.deltaTime;

            if (_timer >= ATTACK_SPEED && player.IsAttackAble)
            {
                player.Attack(_target.TF.position);
            } 
            else if (_timer >= ATTACK_TIME)
            {
                player.ChangeState(new PlayerIdleState());
            }
        }

        public void OnExit(Player.Player player)
        {
            _target.SetCircleTargetIndicator(false);
        }
    }
}