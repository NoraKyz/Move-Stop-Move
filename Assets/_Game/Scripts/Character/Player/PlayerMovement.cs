using _Game.Scripts.Input;
using _Pattern;
using _Pattern.Pool.Scripts;
using _UI.Scripts.GamePlay;
using _UI.Scripts.UI;
using UnityEngine;

namespace _Game.Scripts.Character.Player
{
    public class PlayerMovement : GameUnit, IPlayerMovement
    {
        #region Config

        [Header("References")]
        [SerializeField] private CharacterController controller;
        
        [Header("Config")]
        [SerializeField] private float moveSpeed = 5f;
        
        [Header("Validation")]
        [SerializeField] private bool isFailedConfig;
        
        private bool _moveAble;
        private bool _isStartMove;
        private Vector3 _moveDirection;
        public bool IsMoving => _moveDirection != Vector3.zero;
        
#if UNITY_EDITOR
        private void OnValidate() 
        {
            Common.Warning(controller != null, this, "Missing reference: controller");
            isFailedConfig = controller == null;
        }    
        
#endif
        
        #endregion
        
        #region Init
        
        private void Start() 
        {
            if (isFailedConfig)
            {
                return;
            }
            
            OnInit();
        }
        private void OnInit()
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
                UIManager.Instance.GetUI<GamePlay>().HideTutorial();
            }
        }
        public void Move()
        {
            controller.Move(_moveDirection * (Time.deltaTime * moveSpeed));
        }
    }
}