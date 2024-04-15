using _Game.Scripts.Other.Utils;
using _Game.Scripts.Setting.Sound;
using _SDK.StateMachine;
using _SDK.UI.Base;
using _SDK.UI.Revive;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Character.State.PlayerState
{
    public class PlayerDieState : IState<Player.Player>
    {
        private const float DESPAWN_TIME = 1.5f;
        
        private float _timer;
        private bool _isDespawn;
        
        public void OnEnter(Player.Player player)
        {
            _timer = 0;
            _isDespawn = false;

            player.ChangeAnim(AnimName.ANIM_DIE);
            SoundManager.Ins.Play(SoundType.Dead);
            GameManager.ChangeState(GameState.Revive);
        }
        
        public void OnExecute(Player.Player bot)
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
        
        public void OnExit(Player.Player bot)
        {
            
        }
        
        private void Despawn(Player.Player player)
        {
            _isDespawn = true;
            
            player.OnDespawn();
            
            UIManager.Ins.OpenUI<UIRevive>();
        }
    }
}