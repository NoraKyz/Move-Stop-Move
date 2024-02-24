using _Pattern.Pool.Scripts;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Camera
{
    public class CameraFollow : GameUnit
    {
        #region Config

        [Header("References")]
        [SerializeField] private Transform target;
        
        [Header("Config")]
        [SerializeField] private float smoothSpeed = 0.125f;
        [SerializeField] private Vector3 offset;

        #endregion
        
        private void LateUpdate()
        {
            if (target == null)
            {
                return;
            }
            
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(TF.position, desiredPosition, smoothSpeed);
            TF.position = smoothedPosition;
        }
    }
}