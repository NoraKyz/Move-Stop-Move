using _Game.Scripts.Data;
using _SDK.ServiceLocator.Scripts;
using UnityEngine;

namespace _SDK.UI.MainMenu
{
    public class RemoveAdsButton : MonoBehaviour
    {
        private PlayerData PlayerData => this.GetService<DataManager>().PlayerData;
        
        private void OnEnable()
        {
            OnSetup();
        }
        
        private void OnSetup()
        {
            if (PlayerData.IsNoAds == 1)
            {
                gameObject.SetActive(false);
            }
        }

        public void OnClick()
        {
            RemoveAds();
        }
        
        private void RemoveAds()
        {
            PlayerData.IsNoAds = 1;
            gameObject.SetActive(false);
        }
    }
}