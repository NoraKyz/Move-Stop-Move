using System;
using _Game.Scripts.Other.Utils;
using _SDK.Observer.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _SDK.UI.Shop.SkinShop
{
    public class ItemSkinShop : ItemShop
    {
        #region Config

        [SerializeField] private Text iconEquipped;
        [SerializeField] private Image iconLock;
        [SerializeField] private Outline outline;
        [SerializeField] private Button button;

        private Action<object> _onSelectSkinItem;
        private Action<object> _onEquipOtherSkinItem;

        #endregion

        private void Awake()
        {
            button.onClick.AddListener(OnSelect);
        }

        private void OnEnable()
        {
            _onSelectSkinItem = (param) => SetUISelection((ItemSkinShop) param == this);
            this.RegisterListener(EventID.OnSelectItem, _onSelectSkinItem);
            
            _onEquipOtherSkinItem = (param) => OnEquipOtherSkin((ItemSkinShop) param);
            this.RegisterListener(EventID.OnEquipSkinItem, _onEquipOtherSkinItem);
        }
        
        private void OnDisable()
        {
            this.RemoveListener(EventID.OnSelectItem, _onSelectSkinItem);
            this.RemoveListener(EventID.OnEquipSkinItem, _onEquipOtherSkinItem);
        }

        public override void OnInit<T>(ShopType shopType, ItemShopData<T> data, State state)
        {
            base.OnInit(shopType, data, state);
            
            SetUIState(state);
            SetUISelection(false);
        }

        public void OnSelect()
        {
            this.PostEvent(EventID.OnSelectItem, this);
        }
        
        public override void OnEquip()
        {
            base.OnEquip();
            
            PlayerData.EquipItemShop(this);
            this.PostEvent(EventID.OnEquipSkinItem, this);
        }
        
        private void OnEquipOtherSkin(ItemSkinShop itemSkin)
        {
            if (itemSkin == this)
            {
                return;
            }

            if (CurrentState == State.Equipped)
            {
                SetState(State.Unlock);
            }
        }

        
        protected override void SetState(State state)
        {
            base.SetState(state);
            
            SetUIState(state);
        }

        private void SetUIState(State state)
        {
            iconEquipped.enabled = state == State.Equipped;
            iconLock.enabled = state == State.Lock;
        }
        
        private void SetUISelection(bool isSelect)
        {
            outline.enabled = isSelect;
        }
    }
}