using _Framework.Pool.Scripts;
using UnityEngine;

namespace _Game.Scripts.Weapon
{
    public class Weapon : GameUnit
    {
        [SerializeField] private Bullet.Bullet bullet;
        
        private Character.Character _owner;
        
        public void OnInit(Character.Character owner)
        {
            _owner = owner;
        }
        public void SpawnBullet(Vector3 target)
        {
            Bullet.Bullet newBullet = SimplePool.Spawn<Bullet.Bullet>(bullet, _owner.TF.position, Quaternion.identity);
            newBullet.OnInit(_owner, target);
        }
    }
}