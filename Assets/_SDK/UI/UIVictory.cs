using _Game.Scripts.Data;
using _Game.Scripts.Level;
using _Game.Scripts.Other.Utils;
using _Game.Scripts.Setting.Sound;
using _SDK.UI.Base;
using _SDK.UI.MainMenu;

namespace _SDK.UI
{
    public class UIVictory : UICanvas
    {
        public override void Open()
        {
            base.Open();
            
            SoundManager.Ins.Play(SoundType.Win);
            UIManager.Ins.CloseUI<UISetting>();
        }

        public void OnClickContinueBtn()
        {
            SoundManager.Ins.Play(SoundType.ClickButton);

            UpdateCoins(Constants.COIN_PER_GAME);
            GoToMainMenu();
        }

        public void OnClickAdsBtn()
        {
            SoundManager.Ins.Play(SoundType.ClickButton);
 
            UpdateCoins(Constants.COIN_PER_GAME * 3);
            GoToMainMenu();
        }
        
        private void GoToMainMenu()
        {
            GameManager.ChangeState(GameState.MainMenu);
            
            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<UIMainMenu>();
            
            LevelManager.Ins.LoadNextLevel();
        }
    
        private void UpdateCoins(int value)
        {
            PlayerData playerData = DataManager.Ins.PlayerData;
            playerData.Coin += value;
        }
    }
}
