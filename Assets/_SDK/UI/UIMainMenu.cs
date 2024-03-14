using _Game.Scripts.GamePlay.Camera;
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
            
            CameraFollow.Instance.ChangeState(CameraFollow.State.MainMenu);
        }

        public void OnClickPlayBtn()
        {
            GameManager.ChangeState(GameState.GamePlay);
        }
        
        public void OnClickWeaponShopBtn()
        {
            CloseDirectly();
            
            UIManager.Instance.OpenUI<UIWeaponShop>();
        }
        
        public void OnClickSkinShopBtn()
        {
            CloseDirectly();
            
            UIManager.Instance.OpenUI<UISkinShop>();
        }
    }
}
