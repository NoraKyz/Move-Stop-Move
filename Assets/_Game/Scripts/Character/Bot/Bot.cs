using _Pattern.StateMachine;
using _Pattern.StateMachine.BotState;
using UnityEngine;

namespace _Game.Scripts.Character.Bot
{
    public class Bot : Character
    {
        [Header("Components")] 
        [SerializeField] private GameObject circleTargetIndicator;

        private StateMachine<Bot> _stateMachine;
        
        private void Update()
        {
            _stateMachine.UpdateState(this);
        }

        #region Init
        
        public override void OnInit()
        {
            base.OnInit();
            
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
                _stateMachine = new StateMachine<Bot>(this);
            }
    
            _stateMachine.ChangeState(new BotIdleState());
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
