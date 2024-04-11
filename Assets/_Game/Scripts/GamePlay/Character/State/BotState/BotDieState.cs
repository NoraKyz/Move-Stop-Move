using _Game.Scripts.Level;
using _Game.Scripts.Other.Utils;
using _SDK.StateMachine;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Character.State.BotState
{
    public class BotDieState : IState<Bot.Bot>
    {
        private const float DESPAWN_TIME = 1.5f;
        
        private float _timer;
        private bool _isDespawn;
        
        public void OnEnter(Bot.Bot player)
        {
            _timer = 0;
            _isDespawn = false;
            
            player.StopMove();
            player.ChangeAnim(AnimName.ANIM_DIE);
        }
        
        public void OnExecute(Bot.Bot bot)
        {
            if (_isDespawn)
            {
                return;
            }
            
            _timer += Time.deltaTime;
            
            if (_timer >= DESPAWN_TIME)
            {
                Despawn(bot);
            }
        }
        
        public void OnExit(Bot.Bot bot)
        {
            
        }
        
        private void Despawn(Bot.Bot bot)
        {
            _isDespawn = true;
            
            bot.OnDespawn();
            LevelGameManager.Ins.BotDeath(bot);
        }
    }
}