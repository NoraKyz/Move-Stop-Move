using UnityEngine;

namespace _Game.Scripts.Weapon.Bullet
{
    public class RotateBullet : Bullet
    {
        [SerializeField] private Transform model;
        [SerializeField] private float rotateSpeed;

        protected override void Move()
        {
            base.Move();
            Rotate();
        }
        private void Rotate()
        {
            model.Rotate(Vector3.forward, -rotateSpeed * Time.deltaTime);
        }
    }
}