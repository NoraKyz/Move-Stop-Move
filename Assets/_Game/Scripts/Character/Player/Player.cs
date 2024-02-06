using _Pattern.StateMachine;
using _Pattern.StateMachine.PlayerState;
using UnityEngine;

namespace _Game.Scripts.Character.Player
{
    public class Player : Character
    {
        #region Config
        
        [Header("Validation")]
        [SerializeField] private bool isFailedConfig;

        private IPlayerMovement _playerMovement;
        private StateMachine<Player> _stateMachine;
        public bool IsMoving => _playerMovement.IsMoving;
        
        #endregion

        #region Init

        private void Awake()
        {
            _playerMovement = GetComponent<IPlayerMovement>();
            _stateMachine = new StateMachine<Player>(this);
        }

        private void Start()
        {
            if (isFailedConfig)
            {
                return;
            }
            
            OnInit();
        }
        public override void OnInit()
        {
            base.OnInit();

            _stateMachine.ChangeState(new PlayerIdleState());
        }

        #endregion
        
        private void Update()
        {
            _stateMachine.UpdateState(this);
        }
        public override void OnHit()
        {
            base.OnHit();
            
            ChangeState(new PlayerDieState());
        }
        public void Move()
        {
            _playerMovement.Move();
        }
        public void ChangeState(IState<Player> state)
        {
            _stateMachine.ChangeState(state);
        }
    }
}
