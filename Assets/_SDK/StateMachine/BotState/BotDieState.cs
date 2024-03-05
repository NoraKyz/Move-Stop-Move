using _Game.Scripts.GamePlay.Character.Bot;
using _Game.Scripts.Level;
using _Game.Scripts.Other.Utils;
using UnityEngine;

namespace _SDK.StateMachine.BotState
{
    public class BotDieState : IState<Bot>
    {
        private const float DespawnTime = 1.5f;
        
        private float _timer;
        private bool _isDespawn;
        
        public void OnEnter(Bot player)
        {
            _timer = 0;
            _isDespawn = false;
            
            player.StopMove();
            player.ChangeAnim(AnimName.Die);
        }
        
        public void OnExecute(Bot bot)
        {
            if (_isDespawn)
            {
                return;
            }
            
            _timer += Time.deltaTime;
            
            if (_timer >= DespawnTime)
            {
                Despawn(bot);
            }
        }
        
        public void OnExit(Bot bot)
        {
            
        }
        
        private void Despawn(Bot bot)
        {
            _isDespawn = true;
            
            bot.OnDespawn();
            LevelManager.Instance.BotDeath(bot);
        }
    }
}