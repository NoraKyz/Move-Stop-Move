using _Game.Scripts.Manager.Level;
using _Pattern.StateMachine;
using _Pattern.StateMachine.BotState;
using UnityEngine;
using UnityEngine.AI;

namespace _Game.Scripts.Character.Bot
{
    public class Bot : Character
    {
        [Header("Components")] 
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private GameObject circleTargetIndicator;

        private StateMachine<Bot> _stateMachine;
        
        private Vector3 _destination;
        public bool IsDestination => Vector3.Distance(TF.position, _destination + (TF.position.y - _destination.y) * Vector3.up) < 0.1f;
        
        private void Update()
        {
            _stateMachine.UpdateState(this);
        }

        #region Init

        public override void OnInit()
        {
            base.OnInit();
            
            navMeshAgent.speed = moveSpeed;
            
            InitStateMachine();
            
            HideCircleTargetIndicator();
        }
        public override void OnHit()
        {
            base.OnHit();
            
            ChangeState(new BotDieState());
        }
        public override void OnDespawn()
        {
            base.OnDespawn();
            SimplePool.Despawn(this);
        }
        private void InitStateMachine()
        {
            if (_stateMachine == null)
            {
                _stateMachine = new StateMachine<Bot>();
                _stateMachine.SetOwner(this);
            }
    
            _stateMachine.ChangeState(new BotIdleState());
        }

        #endregion

        #region Controller

        public void MoveToPosition(Vector3 position)
        {
            _destination = position;
            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(_destination);
        }
        public void StopMove()
        {
            navMeshAgent.enabled = false;
        }

        #endregion
        
        public void ChangeState(IState<Bot> state)
        {
            _stateMachine.ChangeState(state);
        }
        public void ShowCircleTargetIndicator()
        {
            circleTargetIndicator.SetActive(true);
        }
        public void HideCircleTargetIndicator()
        {
            circleTargetIndicator.SetActive(false);
        }
    }
}
