
using _UI.Scripts.UI;

namespace _UI.Scripts
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
