using _Game.Scripts.GamePlay.Camera;
using _Game.Scripts.GamePlay.Character;
using _Game.Scripts.Interface;
using _Game.Scripts.Other.Utils;
using _Game.Scripts.Setting.Sound;
using _SDK.Utils;
using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Bullet
{
    public class BombBullet : BulletBase
    {
        private const float MIN_FLYING_TIME = 0.5f;
        private const float MAX_FLYING_TIME = 1f;
        private const float MIN_THROW_POWER = 1.5f;
        private const float MAX_THROW_POWER = 2f;
        
        [SerializeField] private float explosionRadius;
        [SerializeField] private LayerMask hitLayer;

        private Character.Base.Character _owner;
        
        private float _distance;
        private float _flyingTime;
        private float _throwPower;
        
        public override void OnInit(Character.Base.Character owner, Vector3 targetPos)
        {
            _owner = owner;
            
            _distance = Vector3.Distance(_owner.TF.position, targetPos);
            _flyingTime = GetFlyTime(_distance);
            _throwPower = GetThrowPower(_flyingTime);
            
            TF.localScale = Vector3.one * _owner.Size;
            
            Throw(targetPos, _throwPower);
        }

        protected override void OnDetect(Collider other)
        {
            IHit hit = Cache<IHit>.GetComponent(other);
            
            if (hit is not null && hit.IsDie == false && hit != (IHit) _owner)
            {
                Explode();
            }
        }
        
        private void Explode()
        {
            Collider[] results = new Collider[Constants.MAX_BOT_ON_MAP];
            
            // Lay tat ca character trong vung no
            Vector3 explodePos = TF.position;
            Physics.OverlapSphereNonAlloc(explodePos, explosionRadius, results, hitLayer);
            
            // Execute Effect
            Despawn();
            ParticlePool.Play(ParticleType.BombExplosion, explodePos);
            if (CameraFollower.Ins.IsOnScreen(explodePos))
            {
                SoundManager.Ins.Play(SoundType.BombExplosion);
            }

            TF.DOKill();
            
            // Hit all character (tru owner) trong vung no
            for (int i = 0; i < results.Length; i++)
            {
                if (results[i] is null)
                {
                    continue;
                }
                
                IHit hit = Cache<IHit>.GetComponent(results[i]);
            
                if (hit is not null && hit.IsDie == false && hit != (IHit) _owner)
                {
                    _owner.AddScore();
                    hit.OnHit();
                }
            }
        }

        private float GetFlyTime(float distance)
        {
            float minLimitDistance = Constants.DEFAULT_ATTACK_RANGE * _owner.Size / 2;
            float maxLimitDistance = Constants.DEFAULT_ATTACK_RANGE * _owner.Size;
            
            float flyingTime;
            
            if (distance <= minLimitDistance)
            {
                flyingTime = MIN_FLYING_TIME;
            } 
            else if (distance >= maxLimitDistance)
            {
                flyingTime = MAX_FLYING_TIME;
            }
            else
            {
                flyingTime = MIN_FLYING_TIME + (distance - minLimitDistance) / maxLimitDistance * (MAX_FLYING_TIME - MIN_FLYING_TIME);
            }
            
            return flyingTime;
        }
        
        private float GetThrowPower(float flyingTime)
        {
            float throwPower = MIN_THROW_POWER + (flyingTime - MIN_FLYING_TIME) / (MAX_FLYING_TIME - MIN_FLYING_TIME) * (MAX_THROW_POWER - MIN_THROW_POWER);
            
            return throwPower;
        }

        private void Throw(Vector3 targetPos, float throwPower)
        {
            TF.DOJump(targetPos, throwPower, 1, _flyingTime)
                .SetEase(Ease.Linear)
                .OnComplete(Explode);
        }
    }
}