using _Pattern.Event.Scripts;
using _UI.Scripts.UI;

namespace _UI.Scripts
{
    public class MainMenu : UICanvas
    {
        public void PlayButton()
        {
            GameManager.ChangeState(GameState.GamePlay);
            UIManager.Instance.OpenUI<GamePlay>();
            this.PostEvent(EventID.OnGamePlay);
            
            Close(0);
        }
    }
}
