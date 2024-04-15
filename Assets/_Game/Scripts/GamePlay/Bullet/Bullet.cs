using _Game.Scripts.Interface;
using _Game.Scripts.Other.Utils;
using _SDK.Pool.Scripts;
using _SDK.UI.Base;
using _SDK.Utils;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Bullet
{
    public class Bullet : PoolUnit
    {
        #region Config
        
        private const float RANGE_COEFFICIENT = 1.3f;

        [Header("Config")]
        [SerializeField] protected float moveSpeed;
        
        private GamePlay.Character.Base.Character _owner;
        
        protected float range;
        protected Vector3 startPos;
        protected Vector3 moveDirection;
        
        #endregion

        #region Init

        public virtual void OnInit(GamePlay.Character.Base.Character owner, Vector3 targetPos)
        {
            _owner = owner;
            
            SetDirection(targetPos);
            
            // Set khoang cach bay
            range = Constants.DEFAULT_ATTACK_RANGE * _owner.Size * RANGE_COEFFICIENT;
            
            // Set transform model
            TF.rotation = Quaternion.LookRotation(moveDirection);
            TF.localScale = Vector3.one * _owner.Size;
        }
        
        private void SetDirection(Vector3 targetPos)
        {
            startPos = TF.position;
            moveDirection = (targetPos - startPos);
            moveDirection.y = 0;
            moveDirection.Normalize();
        }

        #endregion

        private void Update()
        {
            if (GameManager.IsState(GameState.GamePlay) == false)
            {
                return;
            }
            
            Move();

            if (CanDespawn())
            {
                Despawn();
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            IHit hit = Cache<IHit>.GetComponent(other);
            
            if (hit is not null && !hit.IsDie && hit != (IHit) _owner)
            {
                _owner.AddScore();
                hit.OnHit();
                ParticlePool.Play(ParticleType.Hit, TF.position);

                Despawn();
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
