using _Game.Scripts.GamePlay.Character;
using _Game.Scripts.Setting.Sound;
using _SDK.Observer.Scripts;
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
            CloseDirectly();
            GameManager.ChangeState(GameState.Finish);
            UIManager.Ins.OpenUI<UILose>();
            this.GetService<SoundManager>().Play(SoundType.ClickButton);
        }
        
        public void ReviveBtn()
        {
            CloseDirectly();
            GameManager.ChangeState(GameState.GamePlay);
            this.PostEvent(EventID.OnPlayerRevive);
            this.GetService<SoundManager>().Play(SoundType.ClickButton);
        }
    }
}