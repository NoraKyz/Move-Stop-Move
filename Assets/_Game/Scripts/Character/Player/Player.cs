using _Game.Scripts.Utils;
using _Pattern.StateMachine;
using _Pattern.StateMachine.PlayerState;
using UnityEngine;

namespace _Game.Scripts.Character.Player
{
    public class Player : Character
    {
        [Header("Controller")] 
        [SerializeField] private FloatingJoystick joystick;
        [SerializeField] private CharacterController controller;
        
        private Vector3 _moveDirection;
        private StateMachine<Player> _stateMachine;
        public bool IsMoving => _moveDirection != Vector3.zero;
        
        private void Update()
        {
            GetInput();
            
            _stateMachine.UpdateState(this);
        }
        
        protected override void OnInit()
        {
            base.OnInit();
            
            FindJoyStick();
            
            _stateMachine = new StateMachine<Player>();
            _stateMachine.SetOwner(this);
            _stateMachine.ChangeState(new PlayerIdleState());
        }
        private void FindJoyStick()
        {
            if (joystick == null)
            {
                joystick = FindObjectOfType<FloatingJoystick>();
            }
        }

        #region Controller

        private void GetInput()
        {
            if (Vector2.Distance(joystick.Direction, Vector2.zero) > 0.1f)
            { 
                _moveDirection.Set(joystick.Horizontal, 0, joystick.Vertical);
                _moveDirection.Normalize();
            }
            else
            {
                _moveDirection = Vector3.zero;
            }
        }
        public void Move()
        {
            controller.Move(_moveDirection * (Time.deltaTime * moveSpeed));
            LookAt(TF.position + _moveDirection);
        }

        #endregion
        
        public void ChangeState(IState<Player> state)
        {
            _stateMachine.ChangeState(state);
        }
    }
}
