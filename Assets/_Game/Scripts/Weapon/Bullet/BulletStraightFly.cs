using UnityEngine;

namespace _Game.Scripts.Weapon.Bullet
{
    public class BulletStraightFly : BulletFly
    {
        protected override void Move()
        {
            target.Translate(Vector3.forward * (moveSpeed * Time.deltaTime));
        }
    }
}