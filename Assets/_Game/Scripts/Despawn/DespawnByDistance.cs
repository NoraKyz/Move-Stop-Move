using _Framework.Pool.Scripts;
using UnityEngine;

namespace _Game.Scripts.Despawn
{
    public class DespawnByDistance<T> : Despawn<T> where T : GameUnit, IAutoDespawn
    {
        private Vector3 _startPos;
        protected float distanceDespawn;

        private void OnEnable()
        {
            OnInit();
        }

        protected override bool CanDespawn()
        {
            float distance = Vector3.Distance(_startPos, target.TF.position);
            return distance > distanceDespawn;
        }
        
        protected virtual void OnInit()
        {
            _startPos = target.TF.position;
        }
    }
}