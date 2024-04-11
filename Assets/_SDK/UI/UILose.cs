using _Game.Scripts.Data;
using _Game.Scripts.GamePlay.Character;
using _Game.Scripts.GamePlay.Character.Player;
using _Game.Scripts.Level;
using _Game.Scripts.Other.Utils;
using _Game.Scripts.Setting.Sound;
using _SDK.UI.Base;
using _SDK.UI.MainMenu;
using UnityEngine;
using UnityEngine.UI;

namespace _SDK.UI
{
    public class UILose : UICanvas
    {
        [SerializeField] private Text rankText;
        [SerializeField] private Text killerName;
        [SerializeField] private Text coinsGetText;

        public override void Open()
        {
            base.Open();
            
            SoundManager.Ins.Play(SoundType.Lose);
            
            Player player = CharacterManager.Ins.Player;
    
            rankText.text = "#" + player.Rank;
            killerName.text = player.KillerName;
            coinsGetText.text = Constants.COIN_PER_GAME.ToString();
        }

        public void OnClickContinueBtn()
        {
            UpdateCoins(Constants.COIN_PER_GAME);
            GoToMainMenu();
            SoundManager.Ins.Play(SoundType.ClickButton);
        }

        public void OnClickAdsBtn()
        {
            UpdateCoins(Constants.COIN_PER_GAME * 3);
            GoToMainMenu();
            SoundManager.Ins.Play(SoundType.ClickButton);
        }
        
        private void GoToMainMenu()
        {
            GameManager.ChangeState(GameState.MainMenu);
            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<UIMainMenu>();
            
            PlayerData playerData = DataManager.Ins.PlayerData;
            LevelManager.Ins.LoadLevel(playerData.Level);
        }
        
        private void UpdateCoins(int value)
        {
            PlayerData playerData = DataManager.Ins.PlayerData;
            playerData.Coin += value;
        }
    }
}
