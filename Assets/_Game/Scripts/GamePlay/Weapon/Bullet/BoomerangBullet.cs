using UnityEngine;

namespace _Game.Scripts.GamePlay.Weapon.Bullet
{
    public class BoomerangBullet : RotateBullet
    {
        private bool _isReturning;

        public override void OnInit(Character.Base.Character owner, Vector3 targetPos)
        {
            base.OnInit(owner, targetPos);
            
            _isReturning = false;
        }

        protected override void Move()
        {
            base.Move();

            if (_isReturning)
            {
                return;
            }

            if (CanReturn())
            {
                _isReturning = true;
                moveDirection *= -1; // Reverse direction
            }
        }

        private bool CanReturn()
        {
            return Vector3.Distance(startPos, TF.position) >= range;
        }

        protected override bool CanDespawn()
        {
            if(Vector3.Distance(startPos, TF.position) <= 0.1f && _isReturning)
            {
                return true;
            }

            return false;
        }
    }
}