using System.Collections;
using _Game.Scripts.Manager.Level;
using UnityEngine;
using UnityEngine.UI;

namespace _UI.Scripts
{
    public class GamePlay : UICanvas
    {
        [SerializeField] private Text aliveText;
        [SerializeField] private GameObject tutorial;

        public override void Open()
        {
            base.Open();
            
            int alive = LevelManager.Instance.TotalCharacter;
            SetAliveText(alive);

            StartCoroutine(HideTutorial());
        }

        public override void Close(float delayTime)
        {
            base.Close(delayTime);
            
            tutorial.SetActive(true);
        }

        public void SetAliveText(int alive)
        {
            aliveText.text = alive.ToString();
        }
        
        private IEnumerator HideTutorial()
        {
            yield return new WaitForSeconds(5f);
            tutorial.SetActive(false);
        }
    }
}
