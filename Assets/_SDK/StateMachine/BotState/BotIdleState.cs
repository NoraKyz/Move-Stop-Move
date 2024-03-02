using _Game.Scripts.GamePlay.Character.Bot;
using _Game.Scripts.Other.Utils;
using _SDK.UI.Base;
using UnityEngine;

namespace _SDK.StateMachine.BotState
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
            if (GameManager.IsState(GameState.GamePlay) == false)
            {
                return;
            }
            
            _timer += Time.deltaTime;
            
            if (_timer >= _idleTime)
            {
                bot.ChangeState(new BotPatrolState());
            }
            
            if (bot.HasEnemyInRange && bot.IsAttackAble)
            {
                bot.ChangeState(new BotAttackState());
            }
        }

        public void OnExit(Bot bot)
        {
            
        }
    }
}