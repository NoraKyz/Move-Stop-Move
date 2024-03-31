using _Game.Scripts.Data;
using _Game.Scripts.Other.Utils;
using _SDK.ServiceLocator.Scripts;
using _SDK.UI.Utils;

namespace _SDK.UI.MainMenu
{
    public class SoundButton : ToggleMultipleButton<SettingState>
    {
        private PlayerData PlayerData => this.GetService<DataManager>().PlayerData;
        
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
        }
        
        private void TurnOffSound()
        {
            PlayerData.IsSound = 0;
            SetState(SettingState.Off);
        }
    }
}