using System;
using System.Collections.Generic;
using _Game.Scripts.Data;
using _SDK.Observer.Scripts;
using _SDK.UI.Base;
using UnityEngine;

namespace _SDK.UI.Shop.SkinShop
{
    public enum ShopType
    {
        Hair = 0, 
        Pant = 1, 
        Shield = 2, 
        Set = 3, 
    }
    
    public class UISkinShop : UICanvas
    {
        [SerializeField] private Transform content;
        [SerializeField] private SkinShopItem itemPrefab;
        
        [SerializeField] private SkinShopDataSO skinShopData;
        
        private MiniPool<SkinShopItem> _skinShopItemPool = new MiniPool<SkinShopItem>();
        
        private Action<object> _onSelectBar;
        
        private void Awake()
        {
            _skinShopItemPool.OnInit(itemPrefab, 10, content);
        }

        private void OnEnable()
        {
            _onSelectBar = (param) => InitShop((ShopType) param);
            this.RegisterListener(EventID.OnSelectShopBar, _onSelectBar);
        }
        
        private void OnDisable()
        {
            this.RemoveListener(EventID.OnSelectShopBar, _onSelectBar);
        }

        public override void Open()
        {
            base.Open();
            
            this.PostEvent(EventID.OnSelectShopBar, ShopType.Hair);
        }

        
        private void InitShop(ShopType shopType)
        {
            switch (shopType)
            {
                case ShopType.Hair:
                    InitShopItems(skinShopData.Hairs);
                    break;
                case ShopType.Pant:
                    InitShopItems(skinShopData.Paints);
                    break;
                case ShopType.Shield:
                    InitShopItems(skinShopData.Shields);
                    break;
                case ShopType.Set:
                    InitShopItems(skinShopData.Sets);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void InitShopItems<T>(List<SkinShopData<T>> items) where T : Enum
        {
            _skinShopItemPool.Collect();
            
            for (int i = 0; i < items.Count; i++)
            {
                SkinShopItemState state = UserData.Ins.GetEnumData(items[i].Type.ToString(), SkinShopItemState.UnSelect);
                SkinShopItem item = _skinShopItemPool.Spawn();
                
                item.OnInit(i, items[i], state);
            }
        }
        
        public void OnClickBackBtn()
        {
            CloseDirectly();
            UIManager.Instance.OpenUI<UIMainMenu>();
        }
    }
}