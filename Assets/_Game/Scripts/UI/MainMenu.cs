using _Game.Scripts.UI.Base;

namespace _Game.Scripts.UI
{
    public class MainMenu : UICanvas
    {
        public void PlayButton()
        {
            GameManager.ChangeState(GameState.GamePlay);
        }
    }
}
