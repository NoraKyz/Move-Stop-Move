using System;
using _Game.Scripts.Data;
using _SDK.StateMachine;
using _SDK.StateMachine.PlayerState;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Character.Player
{
    public class Player : GamePlay.Character.Base.Character
    {
        #region Config
        
        [Header("References")]
        [SerializeField] private PlayerMovement playerMovement;
        
        private StateMachine<Player> _stateMachine;
        
        public bool IsMoving => playerMovement.IsMoving;
        
        #endregion

        #region Init

        private void Awake()
        {
            _stateMachine = new StateMachine<Player>(this);
        }

        public override void OnInit()
        {
            base.OnInit();
            
            playerMovement.OnInit();
            _stateMachine.ChangeState(new PlayerIdleState());
        }

        #endregion
        
        private void Update()
        {
            _stateMachine.UpdateState(this);
        }
        
        public override void OnHit(Action hitAction)
        {
            base.OnHit(hitAction);
            
            ChangeState(new PlayerDieState());
        }

        public void Move() => playerMovement.Move();
        
        public void ChangeState(IState<Player> state) => _stateMachine.ChangeState(state);
    }
}
