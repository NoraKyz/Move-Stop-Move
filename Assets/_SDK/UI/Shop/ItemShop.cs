using System;
using _Game.Scripts.Data;
using _Game.Scripts.Other.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace _SDK.UI.Shop
{
    public abstract class ItemShop : MonoBehaviour
    {
        [SerializeField] protected Image imageItem;
        
        public enum State
        {
            Lock = ButtonActionShop.State.Buy,
            Unlock = ButtonActionShop.State.Equip,
            Equipped = ButtonActionShop.State.Equipped
        }
        
        public Enum ShopType { get; protected set; }
        public Enum ItemType { get; protected set; }
        public int Cost { get; protected set; }
        public State CurrentState { get; protected set; }

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
            UserData.Ins.SetItem(this);
        }
        
        protected virtual void SetState(State state)
        {
            CurrentState = state;
            UserData.Ins.SetEnumData(ItemType.ToString(), state);
        }
    }
}