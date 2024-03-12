using _Game.Scripts.GamePlay.Camera;
using _SDK.UI.Base;
using _SDK.UI.Shop.SkinShop;

namespace _SDK.UI
{
    public class UIMainMenu : UICanvas
    {
        public override void Open()
        {
            base.Open();
            
            CameraFollow.Instance.ChangeState(CameraFollow.State.MainMenu);
        }

        public void PlayBtn()
        {
            GameManager.ChangeState(GameState.GamePlay);
        }
        
        public void SkinShopBtn()
        {
            CloseDirectly();
            
            UIManager.Instance.OpenUI<UISkinShop>();
        }
    }
}
