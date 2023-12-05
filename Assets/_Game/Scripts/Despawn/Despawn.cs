using _Framework.Pool.Scripts;
using UnityEngine;

namespace _Game.Scripts.Despawn
{
    public abstract class Despawn<T> : MonoBehaviour where T : GameUnit, IAutoDespawn
    {
        [SerializeField] protected T target;
        
        private void Update()
        {
            Despawning();
        }
        
        private void Despawning()
        {
            if (CanDespawn())
            {
                target.OnDespawn();
            }
        }
        
        protected abstract bool CanDespawn();
    }
}
