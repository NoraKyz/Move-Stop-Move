using UnityEngine;

namespace _Game.Scripts.Weapon
{
    public class WeaponFly : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] private float moveSpeed;
        
        private void Update()
        {
            
        }

        private void Move()
        {
            parent.Translate(Vector3.forward * (moveSpeed * Time.deltaTime));
        }
    }
}