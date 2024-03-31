using _Game.Scripts.Setting.Sound;
using _SDK.Observer.Scripts;
using _SDK.ServiceLocator.Scripts;
using _SDK.UI.Base;
using _SDK.UI.MainMenu;
using UnityEngine;

namespace _SDK.UI.Shop.SkinShop
{
    public class UISkinShop : UICanvas
    {
        [SerializeField] private ShopBarSkin shopBarSkin;

        public override void Open()
        {
            base.Open();
            
            shopBarSkin.OnInit();
        }

        public void OnClickCloseBtn()
        {
            CloseDirectly();
            UIManager.Ins.OpenUI<UIMainMenu>();
            this.PostEvent(EventID.OnCloseShop);
            this.GetService<SoundManager>().Play(SoundType.ClickButton);
        }
    }
}