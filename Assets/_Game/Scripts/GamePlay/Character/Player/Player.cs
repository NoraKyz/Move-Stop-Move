using System;
using _Game.Scripts.Data;
using _Game.Scripts.GamePlay.Camera;
using _Game.Scripts.GamePlay.Skin.Base;
using _Game.Scripts.Other.Utils;
using _SDK.Observer.Scripts;
using _SDK.ServiceLocator.Scripts;
using _SDK.StateMachine;
using _SDK.StateMachine.PlayerState;
using _SDK.UI;
using _SDK.UI.Base;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Character.Player
{
    public class Player : GamePlay.Character.Base.Character
    {
        #region Config
        
        private const string PlayerName = "You";
        
        [Header("References")]
        [SerializeField] private PlayerMovement playerMovement;
        
        [Header("Config")]
        [SerializeField] protected SkinDataSO<PlayerSkin> skinData;
        
        private SetSkinType _currentSkinType;
        private StateMachine<Player> _stateMachine;
        private Action<object> _onCloseSkinShop;
        
        private PlayerData PlayerData => this.GetService<DataManager>().PlayerData;
        
        public bool IsMoving => playerMovement.IsMoving;
        public string KillerName { get; private set; }
        public int Rank { get; private set; }
        
        #endregion

        private void Awake()
        {
            _stateMachine = new StateMachine<Player>(this);
        }
        
        private void OnEnable()
        {
            _onCloseSkinShop = (_) => SetCurrentSkin();
            this.RegisterListener(EventID.OnCloseShop, _onCloseSkinShop);
        }

        private void OnDisable()
        {
            this.RemoveListener(EventID.OnCloseShop, _onCloseSkinShop);
        }

        public override void OnInit()
        {
            base.OnInit();
            
            SetSize(MinSize);
            SetName(PlayerName);
            SetCurrentSkin();
            
            playerMovement.OnInit();
            _stateMachine.ChangeState(new PlayerIdleState());
        }
        
        private void SetCurrentSkin()
        {
            int currentSetSkinId = PlayerData.GetItemEquipped(ItemType.SetSkin);
            SetSkin((SetSkinType) currentSetSkinId);
        }

        public void SetSkin(SetSkinType setSkinType)
        {
            if (characterSkin != null)
            {
                if (_currentSkinType == setSkinType)
                {
                    return;
                }
                
                Destroy(characterSkin.gameObject);
            }
            
            _currentSkinType = setSkinType;
            
            characterSkin = Instantiate(skinData.GetSkin((int)setSkinType), TF);
            characterSkin.OnInit(this);
        }

        private void Update()
        {
            _stateMachine.UpdateState(this);
        }
        
        public override void OnHit(Action hitAction, Base.Character killer)
        {
            base.OnHit(hitAction, killer);
            
            KillerName = killer.CharName;
            Rank = UIManager.Ins.GetUI<UIGamePlay>().Alive + 1;
            ChangeState(new PlayerDieState());
        }

        public override void AddScore(int amount = 1)
        {
            base.AddScore(amount);
            
            ParticlePool.Play(ParticleType.Uplevel, TF.position);
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
