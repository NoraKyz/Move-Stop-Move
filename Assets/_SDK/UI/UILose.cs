using _Game.Scripts.Data;
using _Game.Scripts.GamePlay.Character;
using _Game.Scripts.GamePlay.Character.Player;
using _Game.Scripts.Level;
using _SDK.ServiceLocator.Scripts;
using _SDK.UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace _SDK.UI
{
    public class UILose : UICanvas
    {
        [SerializeField] private Text rankText;
        [SerializeField] private Text killerName;

        public override void Open()
        {
            base.Open();
            
            Player player = this.GetService<CharacterManager>().Player;
    
            rankText.text = "#" + player.Rank;
            killerName.text = player.KillerName;
        }

        public void OnClickContinueBtn()
        {
            GoToMainMenu();
        }

        public void OnClickAdsBtn()
        {
            GoToMainMenu();
        }
        
        private void GoToMainMenu()
        {
            GameManager.ChangeState(GameState.MainMenu);
            
            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<UIMainMenu>();
            PlayerData playerData = this.GetService<DataManager>().PlayerData;
            this.GetService<LevelManager>().OnLoadLevel(playerData.Level);
        }
    }
}
