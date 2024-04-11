using _Game.Scripts.Other.Utils;
using _SDK.StateMachine;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Character.State.BotState
{
    public class BotAttackState : IState<Bot.Bot>
    {
        // Thoi gian de hoan thien 1 don tan cong
        private const float ATTACK_TIME = 1f;
        private const float ATTACK_SPEED = 0.4f;
        
        private float _timer;
        private Base.Character _target;
        
        public void OnEnter(Bot.Bot bot)
        {
            _timer = 0;
            _target = bot.GetEnemy();
            
            bot.LookAtTarget(_target.TF.position);
            bot.ChangeAnim(AnimName.ATTACK);
        }

        public void OnExecute(Bot.Bot bot)
        {
            _timer += Time.deltaTime;

            if (_timer >= ATTACK_SPEED && bot.IsAttackAble)
            {
                bot.Attack(_target.TF.position);
            } 
            else if (_timer >= ATTACK_TIME)
            {
                bot.ChangeState(new BotIdleState());
            }
        }

        public void OnExit(Bot.Bot bot)
        {
            
        }
    }
}