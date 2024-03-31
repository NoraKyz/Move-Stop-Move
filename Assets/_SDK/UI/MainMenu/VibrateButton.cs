using _Game.Scripts.Data;
using _Game.Scripts.Other.Utils;
using _Game.Scripts.Setting.Sound;
using _Game.Scripts.Setting.Vibrate;
using _SDK.ServiceLocator.Scripts;
using _SDK.UI.Utils;

namespace _SDK.UI.MainMenu
{
    public class VibrateButton : ToggleMultipleButton<SettingState>
    {
        private PlayerData PlayerData => this.GetService<DataManager>().PlayerData;
        
        protected override void OnSetup()
        {
            currentState = (SettingState) PlayerData.IsVibrate;
            
            SetState(currentState);
        }
        
        public override void OnClick()
        {
            if(currentState == SettingState.On)
            {
                TurnOffVibrate();
            }
            else
            {
                TurnOnVibrate();
            }
        }
        
        private void TurnOnVibrate()
        {
            PlayerData.IsVibrate = 1;
            SetState(SettingState.On);
            this.GetService<SoundManager>().Play(SoundType.ClickButton);
            this.GetService<VibrateManager>().Vibrate();
        }
        
        private void TurnOffVibrate()
        {
            PlayerData.IsVibrate = 0;
            SetState(SettingState.Off);
            this.GetService<SoundManager>().Play(SoundType.ClickButton);
        }
    }
}