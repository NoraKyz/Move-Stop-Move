using System;
using System.Collections.Generic;
using _Game.Scripts.Data;
using _Game.Scripts.GamePlay.Camera;
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

        public override void Open()
        {
            base.Open();
            
            CameraFollow.Instance.ChangeState(CameraFollow.State.Shop);
        }
        
        private void InitShop(ButtonShopBar btn)
        {
            ShopType shopType = btn.ShopType;
            
            switch (shopType)
            {
                case ShopType.Hair:
                    InitShopItems(itemShopData.Hairs, shopType);
                    break;
                case ShopType.Pant:
                    InitShopItems(itemShopData.Paints, shopType);
                    break;
                case ShopType.Shield:
                    InitShopItems(itemShopData.Shields, shopType);
                    break;
                case ShopType.Set:
                    InitShopItems(itemShopData.Sets, shopType);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void InitShopItems<T>(List<ItemShopData<T>> listItemData, ShopType shopType) where T : Enum
        {
            _skinShopItemPool.Collect();
            
            for (int i = 0; i < listItemData.Count; i++)
            {
                ItemShop.State state = UserData.Ins.GetEnumData(listItemData[i].Type.ToString(), ItemShop.State.Lock);
                ItemSkinShop item = _skinShopItemPool.Spawn();
                
                item.OnInit(shopType, listItemData[i], state);
                
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
            UIManager.Instance.OpenUI<UIMainMenu>();
        }
    }
}