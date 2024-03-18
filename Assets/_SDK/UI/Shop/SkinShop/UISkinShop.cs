using System;
using System.Collections.Generic;
using _Game.Scripts.Data;
using _Game.Scripts.Other.Utils;
using _SDK.Observer.Scripts;
using _SDK.UI.Base;
using UnityEngine;

namespace _SDK.UI.Shop.SkinShop
{
    public class UISkinShop : UICanvas
    {
        [SerializeField] private Transform content;
        [SerializeField] private ItemSkinShop itemPrefab;
        
        [SerializeField] private ItemShopDataSO itemShopData;
        
        private ItemSkinShop _currentSelect;
        private PlayerData PlayerData => DataManager.Ins.PlayerData;

        private MiniPool<ItemSkinShop> _skinShopItemPool = new();
        
        private Action<object> _onSelectBar;
        
        private void Awake()
        {
            _skinShopItemPool.OnInit(itemPrefab, 10, content);
        }

        private void OnEnable()
        {
            _onSelectBar = (param) => InitShop((ButtonShopBar) param);
            this.RegisterListener(EventID.OnSelectShopBar, _onSelectBar);
        }

        private void OnDisable()
        {
            this.RemoveListener(EventID.OnSelectShopBar, _onSelectBar);
        }
        
        private void InitShop(ButtonShopBar btn)
        {
            ItemType itemType = btn.ItemType;
            
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
                case ItemType.Set:
                    InitShopItems(itemShopData.Sets, itemType);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void InitShopItems<T>(List<ItemShopData<T>> listItemData, ItemType itemType) where T : Enum
        {
            _skinShopItemPool.Collect();
            
            for (int i = 0; i < listItemData.Count; i++)
            {
                ItemShop.State state = (ItemShop.State) PlayerData.GetItemState(itemType, listItemData[i].Id);
                ItemSkinShop item = _skinShopItemPool.Spawn();
                
                item.OnInit(itemType, listItemData[i], state);
                
                // Select first item
                if (i == 0)
                {
                    item.OnSelect();
                }
            }
        }
        
        public void OnClickCloseBtn()
        {
            CloseDirectly();
            this.PostEvent(EventID.OnCloseShop);
            UIManager.Ins.OpenUI<UIMainMenu>();
        }
    }
}