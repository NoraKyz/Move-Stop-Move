using System;
using _Game.Scripts.Data;
using _Game.Scripts.Other.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace _SDK.UI.Shop
{
    public abstract class ItemShop : MonoBehaviour
    {
        public enum State
        {
            Lock = ButtonActionShop.State.Buy,
            Unlock = ButtonActionShop.State.Equip,
            Equipped = ButtonActionShop.State.Equipped
        }
        
        [SerializeField] protected Image imageItem;

        protected PlayerData PlayerData => DataManager.Ins.PlayerData;
 
        public Enum ShopType { get; private set; }
        public Enum ItemType { get; private set; }
        public int Cost { get; private set; }
        public State CurrentState { get; private set; }

        public virtual void OnInit<T>(ShopType shopType, ItemShopData<T> data, State state) where T : Enum
        {
            ShopType = shopType;
            
            ItemType = data.Type;
            Cost = data.Cost;
            imageItem.sprite = data.Sprite;
            
            CurrentState = state;
        }        
        
        public virtual void OnEquip()
        {
            SetState(State.Equipped);
        }
        
        protected virtual void SetState(State state)
        {
            CurrentState = state;
            PlayerData.SetItemState(this, (int) state);
        }
    }
}