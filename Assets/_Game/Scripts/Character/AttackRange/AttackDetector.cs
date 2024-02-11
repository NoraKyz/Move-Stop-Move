using System;
using _Game.Utils;
using _Pattern.Pool.Scripts;
using UnityEngine;

namespace _Game.Scripts.Character.AttackRange
{
    public abstract class AttackDetector : GameUnit
    {
        private Character _owner;
        
        public void OnInit(Character owner)
        {
            _owner = owner;
            
            SetScaleDetector();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagName.Character))
            {
                CharacterEnterRange(other);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(TagName.Character))
            {
                CharacterExitRange(other);
            }
        }
        private void SetScaleDetector()
        {
            TF.localScale = Vector3.one * _owner.AttackRange;
        }
        
        protected bool IsEnemy(Character character)
        {
            return character != _owner && character != null;
        }
        protected void OnEnemyEnterRange(Character enemy)
        {
            _owner.OnEnemyEnterRange(enemy);
        }
        protected void OnEnemyExitRange(Character enemy)
        {
            _owner.OnEnemyExitRange(enemy);
        }
        
        protected abstract void CharacterEnterRange(Collider other);
        protected abstract void CharacterExitRange(Collider other);
    }
}