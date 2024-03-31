using _Game.Scripts.GamePlay.Character;
using _Game.Scripts.Setting.Sound;
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
            this.GetService<SoundManager>().Play(SoundType.ClickButton);
        }
        
        public void ReviveBtn()
        {
            CloseDirectly();
            GameManager.ChangeState(GameState.GamePlay);
            this.GetService<CharacterManager>().ResetPlayer();
            this.GetService<SoundManager>().Play(SoundType.ClickButton);
        }
    }
}