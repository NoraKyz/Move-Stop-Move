using _Game.Scripts.Character.Player;
using _Game.Scripts.Utils;
using UnityEngine;

namespace _Pattern.StateMachine.PlayerState
{
    public class PlayerDieState : IState<Player>
    {
        private const float DespawnTime = 2f;
        
        private float _timer;
        public void OnEnter(Player player)
        {
            player.ChangeAnim(AnimName.Die);
        }

        public void OnExecute(Player player)
        {
            _timer += Time.deltaTime;
            if (_timer >= DespawnTime)
            {
                player.Despawn();
            }
        }

        public void OnExit(Player player)
        {
            
        }
    }
}