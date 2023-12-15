using System.Collections;
using _Game.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace _UI.Scripts.Revive
{
    public class CountdownTimer : MonoBehaviour
    {
        [SerializeField] private Text timerText;
        
        private int _currentTime;

        public void OnInit()
        {
            _currentTime = Constants.TimeToRevive;

            StartCoroutine(CountdownToStart());
        }

        private IEnumerator CountdownToStart()
        {
            while(_currentTime >= 0)
            {
                timerText.text = _currentTime.ToString();
                
                yield return new WaitForSeconds(1f);

                _currentTime--;
            }
        }
    }
}
