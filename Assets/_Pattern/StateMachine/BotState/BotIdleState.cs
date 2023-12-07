using _Game.Scripts.Character.Bot;
using _Game.Utils;
using UnityEngine;

namespace _Pattern.StateMachine.BotState
{
    public class BotIdleState : IState<Bot>
    {
        private float _timer;
        private float _idleTime = Random.Range(3f, 5f);
        public void OnEnter(Bot bot)
        {
            _timer = 0;
            bot.StopMove();
            bot.ChangeAnim(AnimName.Idle);
        }

        public void OnExecute(Bot bot)
        {
            if (bot.HasEnemyInRange && bot.IsAttackAble)
            {
                bot.ChangeState(new BotAttackState());
            }
            
            _timer += Time.deltaTime;
            if (_timer >= _idleTime)
            {
                bot.ChangeState(new BotPatrolState());
            }
        }

        public void OnExit(Bot bot)
        {
            
        }
    }
}