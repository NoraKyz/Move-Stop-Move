using _Game.Scripts.GamePlay.Character.Player;
using _Game.Scripts.Other.Utils;
using _SDK.UI.Base;
using UnityEngine;

namespace _SDK.StateMachine.PlayerState
{
    public class PlayerDieState : IState<Player>
    {
        private const float DespawnTime = 1.5f;
        
        private float _timer;
        private bool _isDespawn;
        
        public void OnEnter(Player player)
        {
            _timer = 0;
            _isDespawn = false;

            player.ChangeAnim(AnimName.Die);
        }
        
        public void OnExecute(Player bot)
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
        
        public void OnExit(Player bot)
        {
            
        }
        
        private void Despawn(Player player)
        {
            _isDespawn = true;
            
            player.OnDespawn();
            GameManager.ChangeState(GameState.Revive);
        }
    }
}