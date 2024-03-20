using System;
using _Game.Scripts.Data;
using _Game.Scripts.Other.Utils;
using _SDK.ServiceLocator.Scripts;
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

        protected PlayerData PlayerData => this.GetService<DataManager>().PlayerData;
 
        public Enum Type { get; private set; }
        public Enum Id { get; private set; }
        public int Cost { get; private set; }
        public State CurrentState { get; private set; }

        public virtual void OnInit<T>(ItemType itemType, ItemShopData<T> itemData, State state) where T : Enum
        {
            Type = itemType;
            
            Id = itemData.Id;
            Cost = itemData.Cost;
            imageItem.sprite = itemData.Sprite;
            
            CurrentState = state;
        }        
        
        public virtual void OnEquip()
        {
            SetState(State.Equipped);
        }
        
        public virtual void SetState(State state)
        {
            CurrentState = state;
            PlayerData.SetItemState((ItemType) Type, Id, (int) state);
        }
    }
}