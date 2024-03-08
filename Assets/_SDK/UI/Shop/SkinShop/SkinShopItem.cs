using System;
using UnityEngine;
using UnityEngine.UI;

namespace _SDK.UI.Shop.SkinShop
{
    public enum SkinShopItemState
    {
        Select = 0,
        UnSelect = 1,
        Equipable = 2,
        Equipped = 3
    }
    
    public class SkinShopItem : MonoBehaviour
    {
        [SerializeField] private Image image;

        private int _id;
        private int _cost;
        private SkinShopItemState _state;
        
        public void OnInit<T>(int id, SkinShopData<T> data, SkinShopItemState state) where T : Enum
        {
            _id = id;
            image.sprite = data.Sprite;
            _cost = data.Cost;
            _state = state; 
        }
    }
}