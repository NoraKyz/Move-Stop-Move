using _Framework.Pool.Scripts;
using _Game.Scripts.Despawn;
using _Game.Scripts.Utils;
using UnityEngine;

namespace _Game.Scripts.Weapon
{
    public class Weapon : GameUnit, IAutoDespawn
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagName.Bot) || other.CompareTag(TagName.Player))
            {
                OnDespawn();
            }
        }

        public void OnDespawn()
        {
            SetScale(1);
            SimplePool.Despawn(this);
        }

        public void SetScale(float scale)
        {
            TF.localScale = Vector3.one * scale;
        }
    }
}
