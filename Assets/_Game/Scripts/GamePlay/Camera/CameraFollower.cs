using _SDK.ServiceLocator.Scripts;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Camera
{
    public class CameraFollower : GameService
    {
        #region Config

        public enum State
        {
            MainMenu = 0, 
            Gameplay = 1, 
            Shop = 2
        }
  
        [Header("References")]
        [SerializeField] Transform tf;
        [SerializeField] private Transform target;
        
        [Header("Offset")]
        [SerializeField] Vector3 offset;
        [SerializeField] Vector3 offsetMin;
        [SerializeField] Vector3 offsetMax;
        
        [SerializeField] Transform[] offsets;
        [SerializeField] float moveSpeed;
        
        private Vector3 _targetOffset;
        private Quaternion _targetRotate;
        private State _currentState;

        #endregion

        private void Start()
        {
            _targetOffset = offset;
            _targetRotate = tf.rotation;
            _currentState = State.MainMenu;
        }

        private void FixedUpdate()
        {
            offset = Vector3.Lerp(offset, _targetOffset, Time.fixedDeltaTime * moveSpeed);
            tf.rotation = Quaternion.Lerp(tf.rotation, _targetRotate, Time.fixedDeltaTime * moveSpeed);
            tf.position = Vector3.Lerp(tf.position, target.position + _targetOffset, Time.fixedDeltaTime * moveSpeed);
        }

        public void SetRateOffset(float rate)
        {
            if (_currentState != State.Gameplay)
            {
                return;
            }
            
            _targetOffset = Vector3.Lerp(offsetMin, offsetMax, rate);
        }

        public void ChangeState(State state)
        {
            _currentState = state;
            _targetOffset = offsets[(int)state].localPosition;
            _targetRotate = offsets[(int)state].localRotation;
        }

        public void SetTarget(Transform nTarget)
        {
            target = nTarget;
        }
    }
}