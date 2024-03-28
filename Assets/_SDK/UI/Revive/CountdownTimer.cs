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
        private Coroutine _countdownCoroutine;

        #endregion

        private void OnDisable()
        {
            if (_countdownCoroutine != null)
            {
                StopCoroutine(_countdownCoroutine);
            }
        }

        public void OnInit()
        {
            _currentTime = Constants.TimeToRevive;

            _countdownCoroutine = StartCoroutine(CountdownToStart());
        }

        private IEnumerator CountdownToStart()
        {
            while(_currentTime >= 0)
            {
                timerText.text = _currentTime.ToString();
                
                yield return new WaitForSeconds(1f);

                _currentTime--;
            }
            
            GameManager.ChangeState(GameState.Finish);
            
            UIManager.Ins.CloseUI<UIRevive>();
            UIManager.Ins.OpenUI<UILose>();
        }
    }
}
