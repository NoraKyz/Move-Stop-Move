using System;
using _Game.Scripts.Data;
using _SDK.Observer.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _SDK.UI.Shop
{
    public class CoinInfo : MonoBehaviour
    {
        [SerializeField] private Text coinText;
        
        private Action<object> _onChangeCoin;

        private void OnEnable()
        {
            _onChangeCoin = (param) => SetCoin((int) param);
            this.RegisterListener(EventID.OnChangeCoin, _onChangeCoin);
            
            OnInit();
        }
        
        private void OnDisable()
        {
            this.RemoveListener(EventID.OnChangeCoin, _onChangeCoin);
        }

        private void OnInit()
        {
            SetCoin(UserData.Ins.Coin);
        }
        
        private void SetCoin(int coin)
        {
            coinText.text = coin.ToString();
        }
    }
}