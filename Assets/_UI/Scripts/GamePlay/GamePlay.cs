using _Game.Scripts.Manager.Level;
using _Pattern.Event.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _UI.Scripts.GamePlay
{
    public class GamePlay : UICanvas
    {
        [SerializeField] private Text aliveText;
        [SerializeField] private GameObject tutorial;
        
        private int _alive;
        
        public override void Open()
        {
            base.Open();
            
            _alive = LevelManager.Instance.TotalCharacter;
            SetAliveText(_alive);
            
            this.RegisterListener(EventID.OnCharacterDie, _ => CharacterDie());
        }
        public override void Close(float delayTime)
        {
            base.Close(delayTime);
            
            this.RemoveListener(EventID.OnCharacterDie, _ => CharacterDie());
            this.RemoveListener(EventID.OnPlayerStartMove, _ => HideTutorial());
            
            ShowTutorial();
        }
        
        private void SetAliveText(int alive)
        {
            aliveText.text = alive.ToString();
        }
        
        private void ShowTutorial()
        {
            tutorial.SetActive(true);
        }
        private void HideTutorial()
        {
            tutorial.SetActive(false);
        }
        private void CharacterDie()
        {
            _alive--;
            SetAliveText(_alive);
        }
    }
}
