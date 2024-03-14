using System;
using _Game.Scripts.Data;
using _Game.Scripts.Other.Utils;
using _SDK.Observer.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _SDK.UI.Shop.SkinShop
{
    public class SkinShopItem : MonoBehaviour
    {
        [SerializeField] private Image imageItem;
        [SerializeField] private Text iconEquipped;
        [SerializeField] private Image iconLock;
        [SerializeField] private Outline outline;
        [SerializeField] private Button button;
        
        public enum State
        {
            Lock = ButtonShop.State.Buy,
            Unlock = ButtonShop.State.Equip,
            Equipped = ButtonShop.State.Equipped
        }
        
        private State _currentState;

        private Action<object> _onSelectSkinItem;
        private Action<object> _onEquipOtherSkinItem;
        
        public Enum ItemType { get; private set; }
        public ShopType ShopType { get; private set; }
        public int Cost { get; private set; }
        public State CurrentState => _currentState;

        private void Awake()
        {
            button.onClick.AddListener(OnSelect);
        }

        private void OnEnable()
        {
            _onSelectSkinItem = (param) => SetUISelection((SkinShopItem) param == this);
            this.RegisterListener(EventID.OnSelectSkinItem, _onSelectSkinItem);
            
            _onEquipOtherSkinItem = (param) => OnEquipOtherSkin((SkinShopItem) param);
            this.RegisterListener(EventID.OnEquipSkinItem, _onEquipOtherSkinItem);
        }
        
        private void OnDisable()
        {
            this.RemoveListener(EventID.OnSelectSkinItem, _onSelectSkinItem);
            this.RemoveListener(EventID.OnEquipSkinItem, _onEquipOtherSkinItem);
        }

        public void OnInit<T>(ShopType shopType, SkinShopData<T> data, State state) where T : Enum
        {
            ShopType = shopType;
            
            ItemType = data.Type;
            Cost = data.Cost;
            imageItem.sprite = data.Sprite;
            
            _currentState = state;
            SetUIState(state);
            
            SetUISelection(false);
        }

        public void OnSelect()
        {
            this.PostEvent(EventID.OnSelectSkinItem, this);
        }
        
        public void OnEquip()
        {
            SetState(State.Equipped);
            UserData.Ins.SetSkinItem(this);
            this.PostEvent(EventID.OnEquipSkinItem, this);
        }
        
        private void OnEquipOtherSkin(SkinShopItem skin)
        {
            if (skin == this)
            {
                return;
            }

            if (_currentState == State.Equipped)
            {
                SetState(State.Unlock);
            }
        }

        private void SetUISelection(bool isSelect)
        {
            outline.enabled = isSelect;
        }
        
        private void SetState(State state)
        {
            _currentState = state;
            SetUIState(state);
            
            UserData.Ins.SetEnumData(ItemType.ToString(), state);
        }

        private void SetUIState(State state)
        {
            iconEquipped.enabled = state == State.Equipped;
            iconLock.enabled = state == State.Lock;
        }
    }
}