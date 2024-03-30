using _Game.Scripts.GamePlay.Character;
using _Game.Scripts.GamePlay.Character.Player;
using _SDK.ServiceLocator.Scripts;
using _SDK.UI.Base;
using UnityEngine;

namespace _SDK.UI.Revive
{
    public class UIRevive : UICanvas
    {
        #region Config

        [Header("References")]
        [SerializeField] private CountdownTimer timer;

        #endregion
        
        public override void Open()
        {
            base.Open();
            
            timer.OnInit();
        }

        public void CloseBtn()
        {
            GameManager.ChangeState(GameState.Finish);
            
            CloseDirectly();
            UIManager.Ins.OpenUI<UILose>();
        }
        
        public void ReviveBtn()
        {
            GameManager.ChangeState(GameState.GamePlay);
            this.GetService<CharacterManager>().ResetPlayer();
            
            CloseDirectly();
        }
    }
}