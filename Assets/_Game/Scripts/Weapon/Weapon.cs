using _Pattern.Pool.Scripts;
using UnityEngine;

namespace _Game.Scripts.Weapon
{
    public class Weapon : PoolUnit
    {
        [SerializeField] private PoolType bulletType;
        
        private Character.Character _owner;

        public void OnInit(Character.Character owner)
        {
            _owner = owner;
        }
        public void SpawnBullet(Vector3 target)
        {
            Bullet.Bullet newBullet = SimplePool.Spawn<Bullet.Bullet>(bulletType, TF.position, Quaternion.identity);
            newBullet.OnInit(_owner, target);
        }
    }
}