using System;
using _Game.Scripts.Data;
using _Game.Scripts.GamePlay.Camera;
using _Game.Scripts.GamePlay.Character.State.PlayerState;
using _Game.Scripts.GamePlay.Skin.Base;
using _Game.Scripts.Other.Utils;
using _Game.Scripts.Setting.Sound;
using _SDK.StateMachine;
using _SDK.UI;
using _SDK.UI.Base;
using _SDK.UI.Shop.SkinShop;
using _SDK.UI.Shop.WeaponShop;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Character.Player
{
    public class Player : GamePlay.Character.Base.Character
    {
        #region Config
        
        private const string PLAYER_NAME = "You";
        
        [Header("References")]
        [SerializeField] private PlayerMovement playerMovement;
        
        [Header("Config")]
        [SerializeField] protected SkinDataSO<PlayerSkin> skinData;
        
        private SetSkinType _currentSkinType;
        private StateMachine<Player> _stateMachine;
        
        private PlayerData PlayerData => DataManager.Ins.PlayerData;
        
        public bool IsMoving => playerMovement.IsMoving;
        public string KillerName { get; private set; }
        public int Rank { get; private set; }
        
        #endregion

        private void Awake()
        {
            _stateMachine = new StateMachine<Player>(this);
            
            UISkinShop.OnCloseShopSkin += SetCurrentSkin;
            UIWeaponShop.OnCloseWeaponShop += SetCurrentSkin;
        }

        public void OnDestroy()
        {
            UISkinShop.OnCloseShopSkin += SetCurrentSkin;
            UIWeaponShop.OnCloseWeaponShop += SetCurrentSkin;
        }

        public override void OnInit()
        {
            base.OnInit();
            
            SetSize(MIN_SIZE);
            SetName(PLAYER_NAME);
            SetCurrentSkin();
            
            playerMovement.OnInit();
            _stateMachine.ChangeState(new PlayerIdleState());
        }

        public override void OnDespawn()
        {
            base.OnDespawn();
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
                if (_currentSkinType != setSkinType)
                {
                    Destroy(characterSkin.gameObject);
                    characterSkin = Instantiate(skinData.GetSkin((int)setSkinType), TF);
                }
            }
            else
            {
                characterSkin = Instantiate(skinData.GetSkin((int)setSkinType), TF);
            }
            
            _currentSkinType = setSkinType;
            characterSkin.OnInit(this);
        }

        private void Update()
        {
            _stateMachine.UpdateState(this);
        }
        
        public override void OnHit()
        {
            base.OnHit();
            
            Rank = UIManager.Ins.GetUI<UIGamePlay>().Alive + 1;
            ChangeState(new PlayerDieState());
        }

        public override void AddScore(int amount = 1)
        {
            base.AddScore(amount);
            
            SoundManager.Ins.Play(SoundType.SizeUp);
            ParticlePool.Play(ParticleType.Uplevel, TF.position);
        }

        protected override void SetSize(float value)
        {
            base.SetSize(value);
            
            CameraFollower.Ins.SetRateOffset((Size - MIN_SIZE) / (MAX_SIZE - MIN_SIZE));
        }

        public void Move()
        {
            playerMovement.Move();
        }

        public void StopMove()
        {
            playerMovement.StopMove();
        }

        public void ChangeState(IState<Player> state)
        {
            _stateMachine.ChangeState(state);
        }
    }
}
