using System;
using _Game.Scripts.Character.TargetIndicator;
using _Pattern;
using _Pattern.StateMachine;
using _Pattern.StateMachine.BotState;
using UnityEngine;

namespace _Game.Scripts.Character.Bot
{
    public class Bot : Character
    {
        #region Config
        
        [Header("References")]
        [SerializeField] private CircleTargetIndicator circleTargetIndicator;

        [Header("Validation")]
        [SerializeField] private bool isFailedConfig;
        
        private IBotMovement _botMovement;
        private StateMachine<Bot> _stateMachine;
        
        public bool IsDestination => _botMovement.IsDestination;

#if UNITY_EDITOR
        private void OnValidate()
        {
            Common.Warning(circleTargetIndicator != null, this, "Missing reference: CircleTargetIndicator");
            isFailedConfig = circleTargetIndicator == null;
        }
        
#endif

        #endregion

        #region Init

        private void Awake()
        {
            _botMovement = GetComponent<IBotMovement>();
            _stateMachine = new StateMachine<Bot>(this);
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

            _stateMachine.ChangeState(new BotIdleState());
        }
 
        #endregion
        
        private void Update()
        {
            _stateMachine.UpdateState(this);
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
        public void MoveToPosition(Vector3 position)
        {
            _botMovement.MoveToPosition(position);
        }
        public void StopMove()
        {
            _botMovement.StopMove();
        }
        public void ChangeState(IState<Bot> state)
        {
            _stateMachine.ChangeState(state);
        }
        public void ShowCircleTargetIndicator()
        {
            circleTargetIndicator.Show();
        }
        public void HideCircleTargetIndicator()
        {
            circleTargetIndicator.Hide();
        }
    }
} 
