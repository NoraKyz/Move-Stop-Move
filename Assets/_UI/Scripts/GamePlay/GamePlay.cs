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
            
            RegisterEvents();
            
            SetAliveText(_alive);
            ShowTutorial();
        }

        public override void CloseDirectly()
        {
            base.CloseDirectly();
            
            RemoveEvents();
        }

        private void RegisterEvents()
        {
            this.RegisterListener(EventID.OnCharacterDie, _ => UpdateTotalCharacter());
            this.RegisterListener(EventID.OnPlayerStartMove, _ => HideTutorial());
        }
        private void RemoveEvents()
        {
            this.RemoveListener(EventID.OnCharacterDie, _ => UpdateTotalCharacter());
            this.RemoveListener(EventID.OnPlayerStartMove, _ => HideTutorial());
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
        private void UpdateTotalCharacter()
        {
            Debug.Log("UpdateTotalCharacter");
            _alive--;
            SetAliveText(_alive);
        }
    }
}
