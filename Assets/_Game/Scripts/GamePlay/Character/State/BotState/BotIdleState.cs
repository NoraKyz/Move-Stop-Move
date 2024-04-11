using _Game.Scripts.Other.Utils;
using _SDK.StateMachine;
using _SDK.UI.Base;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Character.State.BotState
{
    public class BotIdleState : IState<Bot.Bot>
    {
        private float _timer;
        private float _idleTime = Random.Range(3f, 5f);
        
        public void OnEnter(Bot.Bot player)
        {
            _timer = 0;
            
            player.StopMove();
            player.ChangeAnim(AnimName.IDLE);
        }

        public void OnExecute(Bot.Bot bot)
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

        public void OnExit(Bot.Bot bot)
        {
            
        }
    }
}