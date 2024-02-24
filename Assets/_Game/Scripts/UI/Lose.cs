using _Game.Scripts.UI.Base;

namespace _Game.Scripts.UI
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
