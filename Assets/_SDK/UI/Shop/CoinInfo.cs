using _Game.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace _SDK.UI.Shop
{
    public class CoinInfo : MonoBehaviour
    {
        #region Config

        [SerializeField] private Text coinText;
        
        private PlayerData PlayerData => DataManager.Ins.PlayerData;

        #endregion

        private void OnEnable()
        {
            OnInit();
            
            PlayerData.OnCoinChanged += SetCoinText;
        }

        private void OnInit()
        {
            SetCoinText(PlayerData.Coin);
        }

        private void OnDisable()
        {
            PlayerData.OnCoinChanged -= SetCoinText;
        }

        private void SetCoinText(int value)
        {
            coinText.text = value.ToString();
        }
    }
}