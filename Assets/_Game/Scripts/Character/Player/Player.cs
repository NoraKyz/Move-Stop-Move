using _Pattern.Event.Scripts;
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

        private void Start()
        {
            OnInit();
        }
        private void Update()
        {
            GetInput();
            
            _stateMachine.UpdateState(this);
        }

        #region Init

        public override void OnInit()
        {
            base.OnInit();
            
            InitStateMachine();
            
            this.RegisterListener(EventID.OnGamePlay, (_) => FindJoyStick());
            
            TF.position = Vector3.zero;
        }
        private void FindJoyStick()
        {
            if (joystick == null)
            {
                joystick = FindObjectOfType<FloatingJoystick>();
            }
        }
        private void InitStateMachine()
        {
            if (_stateMachine == null)
            {
                _stateMachine = new StateMachine<Player>();
                _stateMachine.SetOwner(this);
            }
            
            _stateMachine.ChangeState(new PlayerIdleState());
        }

        #endregion

        #region Controller

        private void GetInput()
        {
            if (joystick == null)
            {
                return;
            }
            
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
            LookAtTarget(TF.position + _moveDirection);
        }

        #endregion

        public override void OnDespawn()
        {
            base.OnDespawn();
            
            this.RemoveListener(EventID.OnGamePlay, (_) => FindJoyStick());
        }

        public override void OnHit()
        {
            base.OnHit();
            ChangeState(new PlayerDieState());
        }
        public void ChangeState(IState<Player> state)
        {
            _stateMachine.ChangeState(state);
        }
    }
}
