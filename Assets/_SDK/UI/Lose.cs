using _SDK.UI.Base;

namespace _SDK.UI
{
    public class Lose : UICanvas
    {
        public void OnClickContinueBtn()
        {
            GameManager.ChangeState(GameState.MainMenu);
        }

        public void OnClickAdsBtn()
        {
            GameManager.ChangeState(GameState.MainMenu);
        }
    }
}
