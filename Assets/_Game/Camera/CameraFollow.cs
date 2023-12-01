using _Framework.Pool.Scripts;
using UnityEngine;

namespace _Game.Camera
{
    public class CameraFollow : GameUnit
    {
        [SerializeField] private Transform target;
        [SerializeField] private float smoothSpeed = 0.125f;
        [SerializeField] private Vector3 offset;
        
        private void LateUpdate()
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(TF.position, desiredPosition, smoothSpeed);
            TF.position = smoothedPosition;
        }
    }
}