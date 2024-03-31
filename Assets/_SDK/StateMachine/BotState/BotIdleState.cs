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
        
        public void OnEnter(Bot player)
        {
            _timer = 0;
            
            player.StopMove();
            player.ChangeAnim(AnimName.Idle);
        }

        public void OnExecute(Bot bot)
        {
            _timer += Time.deltaTime;
            
            if (_timer >= _idleTime)
            {
                bot.ChangeState(new BotPatrolState());
            }
            
            if (GameManager.IsState(GameState.GamePlay) == false)
            {
                return;
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