using _Framework.Pool.Scripts;
using _Game.Scripts.Despawn;
using _Game.Scripts.Utils;
using _Pattern;
using UnityEngine;

namespace _Game.Scripts.Weapon.Bullet
{
    public class Bullet : GameUnit, IAutoDespawn
    {
        private Character.Character _owner;
        private float _range;
        public float Range => _range;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagName.Character))
            {
                Character.Character character = Cache<Character.Character>.GetComponent(other);
                
                if (character != _owner)
                {
                    character.OnHit();
                    OnDespawn();
                }
            }
            else
            {
                OnDespawn(); // trigger with platform
            }
        }
        public void OnInit(Character.Character owner, Vector3 target)
        {
            _owner = owner;
            _range = _owner.AttackRange;
            SetSize(_owner.Size);
            TF.LookAt(target);
        }
        public void OnDespawn()
        {
            SimplePool.Despawn(this);
        }

        private void SetSize(float size)
        {
            TF.localScale = Vector3.one * size;
        }
    }
}
