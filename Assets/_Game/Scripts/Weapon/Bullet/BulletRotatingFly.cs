using UnityEngine;

namespace _Game.Scripts.Weapon.Bullet
{
    public class BulletRotatingFly : BulletFly
    {
        [SerializeField] protected float rotateSpeed;
        protected override void Move()
        {
            model.Rotate(Vector3.up * (rotateSpeed * Time.deltaTime));
            target.Translate(Vector3.forward * (moveSpeed * Time.deltaTime));
        }
    }
}