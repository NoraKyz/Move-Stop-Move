using _Game.Scripts.Character.Bot;
using _Game.Utils;
using UnityEngine;

namespace _Pattern.StateMachine.BotState
{
    public class BotAttackState : IState<Bot>
    {
        private const float AttackTime = 1f;
        private const float AttackSpeed = 0.4f;
        
        private float _timer;
        private Vector3 _targetPos;
        public void OnEnter(Bot bot)
        {
            _timer = 0;
            _targetPos = bot.GetRandomEnemyPos();
            
            bot.LookAt(_targetPos);
            bot.ChangeAnim(AnimName.Attack);
        }

        public void OnExecute(Bot bot)
        {
            _timer += Time.deltaTime;

            if (_timer >= AttackSpeed && bot.IsAttackAble)
            {
                bot.Attack(_targetPos);
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