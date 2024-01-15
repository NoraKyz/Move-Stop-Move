using System;
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
        
        private Action<object> _onCharacterDie;
        
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
            _onCharacterDie = _ => UpdateTotalCharacter();
            this.RegisterListener(EventID.OnCharacterDie, _onCharacterDie);
        }
        private void RemoveEvents()
        {
            this.RemoveListener(EventID.OnCharacterDie, _onCharacterDie);
        }
        
        private void SetAliveText(int alive)
        {
            aliveText.text = alive.ToString();
        }
        private void ShowTutorial()
        {
            tutorial.SetActive(true);
        }
        public void HideTutorial()
        {
            tutorial.SetActive(false);
        }
        private void UpdateTotalCharacter()
        {
            _alive--;
            SetAliveText(_alive);
        }
    }
}
