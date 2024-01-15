using _Game.Scripts.Manager;
using _Pattern.Event.Scripts;
using _Pattern.StateMachine;
using _Pattern.StateMachine.PlayerState;
using UnityEngine;

namespace _Game.Scripts.Character.Player
{
    public class Player : Character
    {
        [Header("Controller")] 
        [SerializeField] private CharacterController controller;
        
        private StateMachine<Player> _stateMachine;
        
        private bool _isStartMove;
        private Vector3 _moveDirection;
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
            
            _isStartMove = false;
            _moveDirection = Vector3.zero;
        }
        public override void OnHit()
        {
            base.OnHit();
            ChangeState(new PlayerDieState());
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
            if (InputManager.HasInput())
            { 
                _moveDirection.Set(InputManager.HorizontalAxis, 0, InputManager.VerticalAxis);
                _moveDirection.Normalize();
                
                if(_isStartMove == false)
                {
                    _isStartMove = true;
                    
                    this.PostEvent(EventID.OnPlayerStartMove);
                }
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
        
        public void ChangeState(IState<Player> state)
        {
            _stateMachine.ChangeState(state);
        }
    }
}
