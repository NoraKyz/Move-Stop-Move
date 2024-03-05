using _Game.Scripts.GamePlay.Character.Base;
using _Game.Scripts.GamePlay.Character.Bot;
using _Game.Scripts.Other.Utils;
using UnityEngine;

namespace _SDK.StateMachine.BotState
{
    public class BotAttackState : IState<Bot>
    {
        // Thoi gian de hoan thien 1 don tan cong
        private const float AttackTime = 1f;
        private const float AttackSpeed = 0.4f;
        
        private float _timer;
        private Character _target;
        
        public void OnEnter(Bot player)
        {
            _timer = 0;
            _target = player.GetEnemy();
            
            player.LookAtTarget(_target.TF.position);
            player.ChangeAnim(AnimName.Attack);
        }

        public void OnExecute(Bot bot)
        {
            _timer += Time.deltaTime;

            if (_timer >= AttackSpeed && bot.IsAttackAble)
            {
                bot.Attack(_target.TF.position);
            } 
            else if (_timer >= AttackTime)
            {
                bot.ChangeState(new BotIdleState());
            }
        }

        public void OnExit(Bot bot)
        {
            
        }
    }
}