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
        [SerializeField] private SkinShopItem itemPrefab;
        
        [SerializeField] private SkinShopDataSO skinShopData;
        
        private SkinShopItem _currentSelectItem;

        private MiniPool<SkinShopItem> _skinShopItemPool = new();
        
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
                    InitShopItems(skinShopData.Hairs, shopType);
                    break;
                case ShopType.Pant:
                    InitShopItems(skinShopData.Paints, shopType);
                    break;
                case ShopType.Shield:
                    InitShopItems(skinShopData.Shields, shopType);
                    break;
                case ShopType.Set:
                    InitShopItems(skinShopData.Sets, shopType);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void InitShopItems<T>(List<SkinShopData<T>> listItemData, ShopType shopType) where T : Enum
        {
            _skinShopItemPool.Collect();
            
            for (int i = 0; i < listItemData.Count; i++)
            {
                SkinShopItem.State state = UserData.Ins.GetEnumData(listItemData[i].Type.ToString(), SkinShopItem.State.Lock);
                SkinShopItem item = _skinShopItemPool.Spawn();
                
                item.OnInit(shopType, listItemData[i], state);
                
                // Select first item
                if (i == 0)
                {
                    item.OnSelect();
                }
            }
        }
        
        public void OnClickBackBtn()
        {
            CloseDirectly();
            this.PostEvent(EventID.OnCloseShopSkin);
            UIManager.Instance.OpenUI<UIMainMenu>();
        }
    }
}