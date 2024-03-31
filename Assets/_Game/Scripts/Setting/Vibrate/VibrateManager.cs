using _Game.Scripts.Data;
using _SDK.ServiceLocator.Scripts;
using UnityEngine;

namespace _Game.Scripts.Setting.Vibrate
{
    public class VibrateManager : GameService
    {
        private PlayerData PlayerData => this.GetService<DataManager>().PlayerData;

        public void Vibrate()
        {
            if (PlayerData.IsVibrate == 0)
            {
                return;
            }
            
            Handheld.Vibrate();
        }
    }
}