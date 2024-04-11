using _Game.Scripts.Data;
using _Game.Scripts.Other.Utils;
using _Game.Scripts.Setting.Sound;
using _SDK.UI.Utils;

namespace _SDK.UI.MainMenu
{
    public class SoundButton : ToggleMultipleButton<SettingState>
    {
        private PlayerData PlayerData => DataManager.Ins.PlayerData;
        
        protected override void OnSetup()
        {
            currentState = (SettingState) PlayerData.IsSound;
            
            SetState(currentState);
        }
        
        public override void OnClick()
        {
            if(currentState == SettingState.On)
            {
                TurnOffSound();
            }
            else
            {
                TurnOnSound();
            }
        }
        
        private void TurnOnSound()
        {
            PlayerData.IsSound = 1;
            SetState(SettingState.On);
            SoundManager.Ins.Play(SoundType.ClickButton);
        }
        
        private void TurnOffSound()
        {
            PlayerData.IsSound = 0;
            SetState(SettingState.Off);
        }
    }
}