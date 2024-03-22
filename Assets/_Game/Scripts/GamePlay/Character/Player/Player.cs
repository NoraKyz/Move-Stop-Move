using System;
using _Game.Scripts.GamePlay.Camera;
using _SDK.ServiceLocator.Scripts;
using _SDK.StateMachine;
using _SDK.StateMachine.PlayerState;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Character.Player
{
    public class Player : GamePlay.Character.Base.Character
    {
        #region Config
        
        public const string PlayerName = "You";
        
        [Header("References")]
        [SerializeField] private PlayerMovement playerMovement;
        
        private StateMachine<Player> _stateMachine;
        
        public bool IsMoving => playerMovement.IsMoving;
        
        #endregion

        private void Awake()
        {
            _stateMachine = new StateMachine<Player>(this);
        }

        public override void OnInit()
        {
            base.OnInit();
            
            SetSize(MinSize);
            
            playerMovement.OnInit();
            targetIndicator.SetName(PlayerName);
            _stateMachine.ChangeState(new PlayerIdleState());
        }

        private void Update()
        {
            _stateMachine.UpdateState(this);
        }
        
        public override void OnHit(Action hitAction)
        {
            base.OnHit(hitAction);
            
            ChangeState(new PlayerDieState());
        }

        protected override void SetSize(float value)
        {
            base.SetSize(value);
            this.GetService<CameraFollower>().SetRateOffset((Size - MinSize) / (MaxSize - MinSize));
        }

        public void Move() => playerMovement.Move();
        
        public void StopMove() => playerMovement.StopMove();
        
        public void ChangeState(IState<Player> state) => _stateMachine.ChangeState(state);
    }
}
