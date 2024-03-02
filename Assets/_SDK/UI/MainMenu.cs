using _SDK.UI.Base;

namespace _SDK.UI
{
    public class MainMenu : UICanvas
    {
        public void PlayButton()
        {
            GameManager.ChangeState(GameState.GamePlay);
        }
    }
}
