using _Game.Scripts.Interface;
using _Game.Scripts.Other.Utils;
using _Pattern;
using _Pattern.Pool.Scripts;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Weapon.Bullet
{
    public class Bullet : PoolUnit
    {
        #region Config

        [Header("Config")]
        [SerializeField] private float moveSpeed;

        private GamePlay.Character.Base.Character _owner;
        
        private Vector3 _startPos;
        private Vector3 _moveDirection;
        
        #endregion

        #region Init

        public void OnInit(GamePlay.Character.Base.Character owner, Vector3 targetPos, float size)
        {
            _owner = owner;
            
            _moveDirection = (targetPos - TF.position).normalized;
            _moveDirection.y = 0;
            
            TF.rotation = Quaternion.LookRotation(_moveDirection);
            TF.localScale = Vector3.one * size;
        }

        #endregion

        private void Update()
        {
            Move();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagName.Character))
            {
                IHit hit = Cache<IHit>.GetComponent(other);
                
                if (hit != null && hit != (IHit) _owner)
                {
                    hit.OnHit(null);
                    OnDespawn();
                }
            }
        }
        
        
        private void OnDespawn()
        {
            SimplePool.Despawn(this);
        }
        
        protected virtual void Move()
        {
            TF.position += _moveDirection * (moveSpeed * Time.deltaTime);
        }
    }
}
