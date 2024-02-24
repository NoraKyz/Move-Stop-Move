using _Game.Scripts.GamePlay.Input;
using _Game.Scripts.UI.Base;
using _Pattern.Pool.Scripts;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Character.Player
{
    public class PlayerMovement : GameUnit
    {
        #region Config

        [Header("References")]
        [SerializeField] private CharacterController controller;
        
        [Header("Config")]
        [SerializeField] private float moveSpeed = 5f;
        
        private bool _moveAble;
        private bool _isStartMove;
        private Vector3 _moveDirection;
        public bool IsMoving => _moveDirection != Vector3.zero;

        #endregion
        
        #region Init
        
        public void OnInit()
        {
            _moveAble = false;
            _isStartMove = false;
            _moveDirection = Vector3.zero;
        }

        #endregion
        
        private void Update()
        {
            if (!_moveAble && !GameManager.IsState(GameState.GamePlay))
            {
                return;
            }
            
            GetInput();
        }
        private void GetInput()
        {
            if (InputManager.HasInput())
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
            _moveDirection.Set(InputManager.HorizontalAxis, 0, InputManager.VerticalAxis);
            _moveDirection.Normalize();
        }
        private void OnStartMove()
        {
            if(_isStartMove == false)
            {
                _isStartMove = true;
                UIManager.Instance.GetUI<UI.GamePlay.GamePlay>().SetTutorial(false);
            }
        }
        public void Move()
        {
            controller.Move(_moveDirection * (Time.deltaTime * moveSpeed));
        }
    }
}