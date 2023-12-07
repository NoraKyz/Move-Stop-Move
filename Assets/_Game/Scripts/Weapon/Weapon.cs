using _Framework.Pool.Scripts;
using UnityEngine;

namespace _Game.Scripts.Weapon
{
    public class Weapon : GameUnit
    {
        [SerializeField] private PoolType bulletType;
        
        private Character.Character _owner;

        public void OnInit(Character.Character owner)
        {
            _owner = owner;
        }
        public void SpawnBullet(Vector3 target)
        {
            // Unknown Bug when call TF.position
            Bullet.Bullet newBullet = SimplePool.Spawn<Bullet.Bullet>(PoolType.Hammer, _owner.TF.position + Vector3.up, Quaternion.identity);
            newBullet.OnInit(_owner, target);
        }
    }
}