using _Game.Scripts.GamePlay.Character;
using _Game.Scripts.Interface;
using _Game.Scripts.Other.Utils;
using _Game.Scripts.Setting.Sound;
using _SDK.Pool.Scripts;
using _SDK.ServiceLocator.Scripts;
using _SDK.UI.Base;
using _SDK.Utils;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Bullet
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
            moveDirection = (targetPos - startPos).normalized;
            moveDirection.y = 0;
            
            range = Constants.DefaultAttackRange * owner.Size * RangeCoefficient;
            
            TF.rotation = Quaternion.LookRotation(moveDirection);
            TF.localScale = Vector3.one * owner.Size;
            
            
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
            if (other.CompareTag(TagName.Character))
            {
                IHit hit = Cache<IHit>.GetComponent(other);
                
                if (hit != null && hit != (IHit) _owner)
                {
                    hit.OnHit(() => 
                    {
                        _owner.AddScore();
                        ParticlePool.Play(ParticleType.Hit, TF.position);
                        
                        // Nếu là đạn của Player thì mới phát âm thanh
                        if (_owner == this.GetService<CharacterManager>().Player)
                        {
                            this.GetService<SoundManager>().Play(SoundType.WeaponHit);
                        }
                        
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
