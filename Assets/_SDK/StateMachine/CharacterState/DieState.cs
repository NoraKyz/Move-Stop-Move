using _Game.Scripts.GamePlay.Character.Base;
using _Game.Scripts.Other.Utils;
using UnityEngine;

namespace _SDK.StateMachine.CharacterState
{
    public class DieState<T> : IState<T> where T : Character
    {
        private const float DespawnTime = 1.5f;
        
        private float _timer;
        private bool _isDespawn;
        
        public virtual void OnEnter(T character)
        {
            _timer = 0;
            _isDespawn = false;
            
            character.ChangeAnim(AnimName.Die);
        }
        
        public void OnExecute(T character)
        {
            if (_isDespawn)
            {
                return;
            }
            
            _timer += Time.deltaTime;
            
            if (_timer >= DespawnTime)
            {
                Despawn(character);
            }
        }
        
        public void OnExit(T character)
        {
            
        }
        
        protected virtual void Despawn(T character)
        {
            _isDespawn = true;
            
            character.OnDespawn();
        }
    }
}