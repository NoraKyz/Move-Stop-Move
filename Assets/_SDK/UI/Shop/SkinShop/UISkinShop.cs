using System;
using _Game.Scripts.Setting.Sound;
using _SDK.UI.Base;
using _SDK.UI.MainMenu;
using UnityEngine;

namespace _SDK.UI.Shop.SkinShop
{
    public class UISkinShop : UICanvas
    {
        public static event Action OnCloseShopSkin;
        
        [SerializeField] private ShopBarSkin shopBarSkin;

        public override void Open()
        {
            base.Open();
            
            shopBarSkin.OnInit();
        }

        public void OnClickCloseBtn()
        {
            CloseDirectly();
            OnCloseShopSkin?.Invoke();
            UIManager.Ins.OpenUI<UIMainMenu>();
            SoundManager.Ins.Play(SoundType.ClickButton);
        }
    }
}