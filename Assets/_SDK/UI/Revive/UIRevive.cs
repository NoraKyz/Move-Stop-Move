using System;
using _Game.Scripts.GamePlay.Character;
using _Game.Scripts.Setting.Sound;
using _SDK.UI.Base;
using UnityEngine;

namespace _SDK.UI.Revive
{
    public class UIRevive : UICanvas
    {
        public static event Action OnPlayerRevive;
        
        #region Config

        [Header("References")]
        [SerializeField] private CountdownTimer timer;

        #endregion
        
        public override void Open()
        {
            base.Open();
            
            timer.OnInit();
            UIManager.Ins.CloseUI<UISetting>();
        }

        public void CloseBtn()
        {
            CloseDirectly();
            GameManager.ChangeState(GameState.Finish);
            UIManager.Ins.OpenUI<UILose>();
            SoundManager.Ins.Play(SoundType.ClickButton);
        }
        
        public void ReviveBtn()
        {
            CloseDirectly();
            OnPlayerRevive?.Invoke();
            GameManager.ChangeState(GameState.GamePlay);
            CharacterManager.Ins.ResetPlayer();
            SoundManager.Ins.Play(SoundType.ClickButton);
        }
    }
}