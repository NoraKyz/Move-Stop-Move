using System;
using System.Collections.Generic;
using _Game.Scripts.Data;
using _Game.Scripts.Other.Utils;
using UnityEngine;

namespace _SDK.UI.Shop.SkinShop
{
    public class ShopSkin : MonoBehaviour
    {
        public static event Action<ItemSkin> OnSelectedItemShopSkin;
        
        [SerializeField] private Transform content;
        [SerializeField] private ItemSkin itemSkinPrefab;
        [SerializeField] private ButtonActionShop buttonActionShop;
        
        [SerializeField] private ItemShopDataSO itemShopData;
        [SerializeField] private ShopType shopType;
        [SerializeField] private List<ItemSkin> items;
        
        private PlayerData PlayerData => DataManager.Ins.PlayerData;
        
        private MiniPool<ItemSkin> _skinShopItemPool = new();
        
        private void Awake()
        {
            _skinShopItemPool.OnInit(itemSkinPrefab, 10, content);
        }
        
        public void InitShop (ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Hair:
                    InitShopItems(itemShopData.Hairs, itemType);
                    break;
                case ItemType.Pant:
                    InitShopItems(itemShopData.Paints, itemType);
                    break;
                case ItemType.Shield:
                    InitShopItems(itemShopData.Shields, itemType);
                    break;
                case ItemType.SetSkin:
                    InitShopItems(itemShopData.Sets, itemType);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            OnSelectItem(items[0]);
        }

        private void InitShopItems<T>(List<ItemShopData<T>> listItemData, ItemType itemType) where T : Enum
        {
            items.Clear();
            _skinShopItemPool.Collect();

            for (int i = 0; i < listItemData.Count; i++)
            {
                ItemShop.State state = (ItemShop.State) PlayerData.GetItemState(itemType, listItemData[i].Id);
                ItemSkin itemSkin = _skinShopItemPool.Spawn();
                
                itemSkin.OnInit(itemType, listItemData[i], state);
                itemSkin.Button.onClick.AddListener(() => OnSelectItem(itemSkin));
                
                items.Add(itemSkin);
            }

            shopType = (ShopType) itemType;
        }
        
        private void OnSelectItem(ItemSkin itemSkin)
        {
            OnSelectedItemShopSkin?.Invoke(itemSkin);
            buttonActionShop.OnSelectItem(itemSkin);
            ReloadUISelection(itemSkin);
        }

        public void ReloadUI()
    
        {
            for (int i = 0; i < items.Count; i++)
            {
                ItemShop.State state = (ItemShop.State) PlayerData.GetItemState((ItemType)shopType, items[i].Id);
                items[i].SetState(state);
                items[i].SetUIEquip();
            }
        }
        
        private void ReloadUISelection(ItemSkin itemSkin)
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].SetUISelection(Equals(items[i].Id, itemSkin.Id));
            }
        }
    }
}