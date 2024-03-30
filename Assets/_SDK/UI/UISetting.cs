using _Game.Scripts.Data;
using _Game.Scripts.Level;
using _SDK.ServiceLocator.Scripts;
using _SDK.UI.Base;

namespace _SDK.UI
{
    public class UISetting : UICanvas
    {

        public override void Setup()
        {
            base.Setup();
            
            GameManager.ChangeState(GameState.Setting);
        }

        public void SoundButton()
        {

        }

        public void VibrateButton()
        {

        }

        public void ContinueButton()
        {
            GameManager.ChangeState(GameState.GamePlay);
            UIManager.Ins.OpenUI<UIGamePlay>();
            
            CloseDirectly();
        }

        public void HomeButton()
        {
            GameManager.ChangeState(GameState.MainMenu);
            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<UIMainMenu>();
            
            PlayerData playerData = this.GetService<DataManager>().PlayerData;
            this.GetService<LevelManager>().LoadLevel(playerData.Level);
        }

    }
}
