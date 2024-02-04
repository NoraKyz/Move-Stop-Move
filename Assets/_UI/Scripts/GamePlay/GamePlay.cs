using System;
using _Game.Scripts.Manager.Level;
using _Pattern.Event.Scripts;
using _UI.Scripts.UI;
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

            SetAliveText(_alive);
            ShowTutorial();
        }
        protected override void RegisterEvents()
        {
            _onCharacterDie = _ => OnCharacterDie();
            this.RegisterListener(EventID.OnCharacterDie, _onCharacterDie);
        }
        protected override void RemoveEvents()
        {
            this.RemoveListener(EventID.OnCharacterDie, _onCharacterDie);
        }
        
        private void ShowTutorial()
        {
            tutorial.SetActive(true);
        }
        public void HideTutorial()
        {
            tutorial.SetActive(false);
        }
        private void OnCharacterDie()
        {
            _alive--;
            SetAliveText(_alive);
        }
        private void SetAliveText(int alive)
        {
            aliveText.text = alive.ToString();
        }
    }
}
