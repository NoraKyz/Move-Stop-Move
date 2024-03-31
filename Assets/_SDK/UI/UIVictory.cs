using _Game.Scripts.Data;
using _Game.Scripts.Level;
using _Game.Scripts.Other.Utils;
using _Game.Scripts.Setting.Sound;
using _SDK.ServiceLocator.Scripts;
using _SDK.UI.Base;
using _SDK.UI.MainMenu;

namespace _SDK.UI
{
    public class UIVictory : UICanvas
    {
        public override void Open()
        {
            base.Open();
            
            this.GetService<SoundManager>().Play(SoundType.Win);
        }

        public void OnClickContinueBtn()
        {
            this.GetService<SoundManager>().Play(SoundType.ClickButton);

            UpdateCoins(Constants.CoinPerGame);
            GoToMainMenu();
        }

        public void OnClickAdsBtn()
        {
            this.GetService<SoundManager>().Play(SoundType.ClickButton);
 
            UpdateCoins(Constants.CoinPerGame * 3);
            GoToMainMenu();
        }
        
        private void GoToMainMenu()
        {
            GameManager.ChangeState(GameState.MainMenu);
            
            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<UIMainMenu>();
            
            this.GetService<LevelManager>().LoadNextLevel();
        }
    
        private void UpdateCoins(int value)
        {
            PlayerData playerData = this.GetService<DataManager>().PlayerData;
            playerData.Coin += value;
        }
    }
}
