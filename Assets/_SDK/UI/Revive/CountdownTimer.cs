using System.Collections;
using _Game.Scripts.Other.Utils;
using _SDK.UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace _SDK.UI.Revive
{
    public class CountdownTimer : MonoBehaviour
    {
        #region Config

        [Header("References")]
        [SerializeField] private Text timerText;
        
        private int _currentTime;

        #endregion

        #region Init

        public void OnInit()
        {
            _currentTime = Constants.TimeToRevive;

            StartCoroutine(CountdownToStart());
        }
        
        #endregion
        
        private IEnumerator CountdownToStart()
        {
            while(_currentTime >= 0)
            {
                timerText.text = _currentTime.ToString();
                
                yield return new WaitForSeconds(1f);

                _currentTime--;
            }
            
            GameManager.ChangeState(GameState.Lose);
        }
    }
}
