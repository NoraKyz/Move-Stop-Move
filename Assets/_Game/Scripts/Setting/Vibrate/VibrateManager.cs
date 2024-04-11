using _Game.Scripts.Data;
using _SDK.Singleton;
using UnityEngine;

namespace _Game.Scripts.Setting.Vibrate
{
    public class VibrateManager : Singleton<VibrateManager>
    {
        private PlayerData PlayerData => DataManager.Ins.PlayerData;

        public void Vibrate()
        {
            if (PlayerData.IsVibrate == 0 && SystemInfo.supportsVibration)
            {
                return;
            }
            
            Handheld.Vibrate();
        }
    }
}