using _Game.Scripts.Interface;
using _Game.Scripts.Other.Utils;
using _SDK.Pool.Scripts;
using _SDK.Utils;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Weapon.Bullet
{
    public class Bullet : PoolUnit
    {
        #region Config
        
        private const float RangeCoefficient = 1.3f;

        [Header("Config")]
        [SerializeField] protected float moveSpeed;

        [SerializeField] protected float range;

        private GamePlay.Character.Base.Character _owner;
        
        protected Vector3 startPos;
        protected Vector3 moveDirection;
        
        #endregion

        #region Init

        public virtual void OnInit(GamePlay.Character.Base.Character owner, Vector3 targetPos)
        {
            _owner = owner;
            
            startPos = TF.position;
            moveDirection = (targetPos - TF.position).normalized;
            moveDirection.y = 0;
            
            TF.rotation = Quaternion.LookRotation(moveDirection);
            TF.localScale = Vector3.one * owner.Size;
            
            range = Constants.DefaultAttackRange * owner.Size * RangeCoefficient;
        }

        #endregion

        private void Update()
        {
            Move();

            if (CanDespawn())
            {
                Despawn();
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagName.Character))
            {
                IHit hit = Cache<IHit>.GetComponent(other);
                
                if (hit != null && hit != (IHit) _owner)
                {
                    hit.OnHit(() => 
                    {
                        _owner.AddScore();
                    }, _owner);
                    Despawn();
                }
            }
        }
        
        private void Despawn()
        {
            SimplePool.Despawn(this);
        }
        
        protected virtual void Move()
        {
            TF.position += moveDirection * (moveSpeed * Time.deltaTime);
        }

        protected virtual bool CanDespawn()
        {
            return Vector3.Distance(startPos, TF.position) >= range;
        }
    }
}
