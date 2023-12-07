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
        
        private Vector3 _destination;
        private StateMachine<Bot> _stateMachine;
        public bool IsDestination => Vector3.Distance(TF.position, _destination + (TF.position.y - _destination.y) * Vector3.up) < 0.1f;
        
        private void Update()
        {
            _stateMachine.UpdateState(this);
        }
        protected override void OnInit()
        {
            base.OnInit();
            
            _stateMachine = new StateMachine<Bot>();
            _stateMachine.SetOwner(this);
            _stateMachine.ChangeState(new BotIdleState());
        }
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

        public override void OnHit()
        {
            base.OnHit();
            ChangeState(new BotDieState());
        }

        public void ShowCircleTargetIndicator()
        {
            circleTargetIndicator.SetActive(true);
        }
        
        public void HideCircleTargetIndicator()
        {
            circleTargetIndicator.SetActive(false);
        }
        
        public void ChangeState(IState<Bot> state)
        {
            _stateMachine.ChangeState(state);
        }
    }
}
