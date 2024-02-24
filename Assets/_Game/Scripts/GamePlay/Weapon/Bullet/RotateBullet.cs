using UnityEngine;

namespace _Game.Scripts.GamePlay.Weapon.Bullet
{
    public class RotateBullet : Bullet
    {
        #region Config
        
        [Header("References")]
        [SerializeField] private Transform model;

        [Header("Config")]
        [SerializeField] private float rotateSpeed;

        #endregion

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