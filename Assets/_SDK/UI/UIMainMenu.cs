using _Game.Scripts.GamePlay.Camera;
using _SDK.ServiceLocator.Scripts;
using _SDK.UI.Base;
using _SDK.UI.Shop.SkinShop;
using _SDK.UI.Shop.WeaponShop;

namespace _SDK.UI
{
    public class UIMainMenu : UICanvas
    {
        public override void Open()
        {
            base.Open();
            
            this.GetService<CameraFollower>().ChangeState(CameraFollower.State.MainMenu);
        }

        public void OnClickPlayBtn()
        {
            GameManager.ChangeState(GameState.GamePlay);
        }
        
        public void OnClickWeaponShopBtn()
        {
            CloseDirectly();
            UIManager.Ins.OpenUI<UIWeaponShop>();
        }
        
        public void OnClickSkinShopBtn()
        {
            CloseDirectly();
            UIManager.Ins.OpenUI<UISkinShop>();
            this.GetService<CameraFollower>().ChangeState(CameraFollower.State.Shop);
        }
    }
}
