using _SDK.Observer.Scripts;
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
            this.PostEvent(EventID.OnCloseShop);
            UIManager.Ins.OpenUI<UIMainMenu>();
        }
    }
}