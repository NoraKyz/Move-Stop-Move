using _SDK.Pool.Scripts;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Bullet
{
    public abstract class BulletBase : PoolUnit
    {
        public abstract void OnInit(GamePlay.Character.Base.Character owner, Vector3 targetPos);

        private void OnTriggerEnter(Collider other)
        {
            OnDetect(other);
        }
        
        protected abstract void OnDetect(Collider other);
        
        protected void Despawn()
        {
            SimplePool.Despawn(this);
        }
    }
}