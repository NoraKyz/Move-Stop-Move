using _SDK.UI.Base;
using _SDK.UI.Shop.SkinShop;

namespace _SDK.UI
{
    public class UIMainMenu : UICanvas
    {
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
