using _Game.Scripts.GamePlay.Input;
using _SDK.Pool.Scripts;
using _SDK.ServiceLocator.Scripts;
using _SDK.UI;
using _SDK.UI.Base;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.GamePlay.Character.Player
{
    public class PlayerMovement : GameUnit
    {
        #region Config

        [FormerlySerializedAs("rd")]
        [Header("References")]
        [SerializeField] private Rigidbody rb;
        
        [Header("Config")]
        [SerializeField] private float moveSpeed = 5f;
        
        private bool _moveAble;
        private bool _isStartMove;
        private Vector3 _moveDirection;
        
        private InputManager _inputManager;
        
        public bool IsMoving => Vector3.Distance(_moveDirection, Vector3.zero) > 0.01f;

        #endregion

        public void OnInit()
        {
            _isStartMove = false;
            _moveDirection = Vector3.zero;
            _inputManager = this.GetService<InputManager>();
            
            TF.position = Vector3.zero;
            TF.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }

        private void Update()
        {
            if (GameManager.IsState(GameState.GamePlay) == false)
            {
                _moveDirection = Vector3.zero;  
                return;
            }
            
            GetInput();
        }
        
        private void GetInput()
        {
            if (_inputManager.HasInput())
            {
                GetDirectionFromInput();

                OnStartMove();
            }
            else
            {
                _moveDirection = Vector3.zero;
            }
        }
        
        private void GetDirectionFromInput()
        {
            _moveDirection.Set(_inputManager.HorizontalAxis, 0, _inputManager.VerticalAxis);
            _moveDirection.Normalize();
        }
        
        private void OnStartMove()
        {
            if (_isStartMove)
            {
                return;
            }
            
            _isStartMove = true;
            UIManager.Ins.GetUI<UIGamePlay>().SetTutorial(false);
        }
        
        public void Move()
        {
            rb.velocity = _moveDirection * moveSpeed;  
            
            if(_moveDirection != Vector3.zero)
            {
                TF.forward = _moveDirection;
            }
        }

        public void StopMove()
        {
            rb.velocity = Vector3.zero;
        }
    }
}