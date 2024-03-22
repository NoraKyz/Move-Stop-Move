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
            Lock = 0,
            Unlock = 1, 
        }
        
        [SerializeField] protected Image imageItem;
        
        protected PlayerData PlayerData => this.GetService<DataManager>().PlayerData;
 
        public ItemType ItemType { get; private set; }
        public Enum Id { get; private set; }
        public int Cost { get; private set; }
        public State CurrentState { get; protected set; }

        public virtual void OnInit<T>(ItemType itemType, ItemShopData<T> itemData, State state) where T : Enum
        {
            ItemType = itemType;
            
            Id = itemData.Id;
            Cost = itemData.Cost;
            imageItem.sprite = itemData.Sprite;
            
            CurrentState = state;
        } 
        
        public bool IsEquipped()
        {
            int idItemEquipped = PlayerData.GetItemEquipped(ItemType);
            int idItem = Convert.ToInt32(Id);
            
            return idItem == idItemEquipped;
        }
    }
}