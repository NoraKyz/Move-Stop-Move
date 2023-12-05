using UnityEngine;

namespace _Game.Scripts.Weapon.Bullet
{
    public abstract class BulletFly : MonoBehaviour
    {
        [Header("Transforms")]
        [SerializeField] protected Transform target;
        [SerializeField] protected Transform model;
        
        [Header("Config")]
        [SerializeField] protected float moveSpeed;
        
        private void Update()
        {
            Move();
        }

        protected abstract void Move();
    }
}