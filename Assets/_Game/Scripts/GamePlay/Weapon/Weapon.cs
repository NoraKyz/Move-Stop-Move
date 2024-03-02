using _SDK.Pool.Scripts;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Weapon
{
    public class Weapon : GameUnit
    {
        #region Config

        [Header("Config")]
        [SerializeField] private PoolType bulletType;

        #endregion
        
        public void SpawnBullet(Character.Base.Character owner, Vector3 targetPos)
        {
            Bullet.Bullet newBullet = SimplePool.Spawn<Bullet.Bullet>(bulletType, TF.position, Quaternion.identity);
            newBullet.OnInit(owner, targetPos, owner.Size);
        }

        public void SetVisible(bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }
    }
}