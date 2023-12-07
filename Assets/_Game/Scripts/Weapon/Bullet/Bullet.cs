using _Framework.Pool.Scripts;
using _Game.Utils;
using _Pattern;
using UnityEngine;

namespace _Game.Scripts.Weapon.Bullet
{
    public class Bullet : GameUnit
    {
        [SerializeField] private float moveSpeed;
        
        private Character.Character _owner;
        
        // movement
        private Vector3 _startPos;
        private Vector3 _moveDirection;
        private float _maxFlyDistance;

        private void Update()
        {
            Move();

            if (CanDespawn())
            {
                OnDespawn();
            }
        }
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
        
        public void OnInit(Character.Character owner, Vector3 targetPos)
        {
            _owner = owner;
            _startPos = TF.position;
            _maxFlyDistance = owner.AttackRange;
            _moveDirection = (targetPos - _startPos).normalized;
            _moveDirection.y = 0;
            
            TF.rotation = Quaternion.LookRotation(_moveDirection);
            TF.localScale = Vector3.one * owner.AttackRange / Constants.DefaultAttackRange;
        }
        private void OnDespawn()
        {
            SimplePool.Despawn(this);
        }
        protected virtual void Move()
        {
            TF.position += _moveDirection * (moveSpeed * Time.deltaTime);
        }
        protected virtual bool CanDespawn()
        {
            float flyLength = Vector3.Distance(_startPos, TF.position);
            return flyLength >= _maxFlyDistance;
        }
    }
}
