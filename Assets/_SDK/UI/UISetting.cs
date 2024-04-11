using _Game.Scripts.Level;
using _Game.Scripts.Setting.Sound;
using _SDK.UI.Base;
using _SDK.UI.MainMenu;

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
            CloseDirectly();
            GameManager.ChangeState(GameState.GamePlay);
            UIManager.Ins.OpenUI<UIGamePlay>();
            SoundManager.Ins.Play(SoundType.ClickButton);
        }

        public void HomeButton()
        {
            GameManager.ChangeState(GameState.MainMenu);
            
            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<UIMainMenu>();
            
            LevelManager.Ins.LoadCurrentLevel();
            SoundManager.Ins.Play(SoundType.ClickButton);
        }

    }
}
