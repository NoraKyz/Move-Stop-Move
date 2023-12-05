using _Game.Scripts.Weapon.Bullet;

namespace _Game.Scripts.Despawn.Despawner
{
    public class BulletDespawner : DespawnByDistance<Bullet>
    {
        protected override void OnInit()
        {
            base.OnInit();
            distanceDespawn = target.Range;
        }
    }
}