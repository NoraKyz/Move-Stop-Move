using _SDK.Singleton;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Camera
{
    public class CameraFollower : Singleton<CameraFollower>
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
        [SerializeField] private Vector3 targetOffset;
        [SerializeField] Vector3 offsetMin;
        [SerializeField] Vector3 offsetMax;
        
        [SerializeField] Transform[] offsets;
        [SerializeField] float moveSpeed;
        
        private Quaternion _targetRotate;
        private State _currentState;

        #endregion

        private void Start()
        {
            _targetRotate = tf.rotation;
            _currentState = State.MainMenu;
        }

        private void FixedUpdate()
        {
            tf.rotation = Quaternion.Lerp(tf.rotation, _targetRotate, Time.fixedDeltaTime * moveSpeed);
            tf.position = Vector3.Lerp(tf.position, target.position + targetOffset, Time.fixedDeltaTime * moveSpeed);
        }

        public void SetRateOffset(float rate)
        {
            if (_currentState != State.Gameplay)
            {
                return;
            }
            
            targetOffset = Vector3.Lerp(offsetMin, offsetMax, rate);
        }

        public void ChangeState(State state)
        {
            _currentState = state;
            targetOffset = offsets[(int)state].localPosition;
            _targetRotate = offsets[(int)state].localRotation;
        }
    }
}