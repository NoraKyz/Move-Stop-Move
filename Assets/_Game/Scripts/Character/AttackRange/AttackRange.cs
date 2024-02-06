using _Game.Utils;
using _Pattern.Pool.Scripts;
using UnityEngine;

namespace _Game.Scripts.Character
{
    public abstract class AttackRange : PoolUnit
    {
        protected Character owner;
        public void OnInit(Character character)
        {
            owner = character;
            
            TF.localScale = Vector3.one * owner.AttackRange;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagName.Character))
            {
                EnemyEnterRange(other);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(TagName.Character))
            {
                EnemyExitRange(other);
            }
        }
        
        protected abstract void EnemyEnterRange(Collider other);
        protected abstract void EnemyExitRange(Collider other);
    }
}