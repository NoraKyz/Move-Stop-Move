using _Framework;
using _Framework.Pool.Scripts;
using UnityEngine;

namespace _Game.Scripts.Despawn
{
    public class DespawnByDistance<T> : Despawn<T> where T : GameUnit, IAutoDespawn
    {
        [SerializeField] private float distanceDespawn;

        private Vector3 _startPos;

        private void OnEnable()
        {
            _startPos = target.TF.position;
        }

        protected override bool CanDespawn()
        {
            float distance = Vector3.Distance(_startPos, target.TF.position);
            return distance > distanceDespawn;
        }
        
        public void SetDistanceDespawn(float distance)
        {
            if (distance <= 0)
            {
                Common.LogWarning("Distance must be greater than 0", this);
            }
            
            distanceDespawn = distance;
        }
    }
}