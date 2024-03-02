using System;
using _Game.Scripts.Level;
using _Game.Scripts.UI.Base;
using _SDK.Event.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UI.GamePlay
{
    public class GamePlay : UICanvas
    {
        #region Config

        [Header("References")]
        [SerializeField] private Text aliveText;
        [SerializeField] private GameObject tutorial;
        
        private Action<object> _onCharacterDie;
        
        private int _alive;

        #endregion
        
        protected override void RegisterEvents()
        {
            _onCharacterDie = _ => OnCharacterDie();
            this.RegisterListener(EventID.OnCharacterDie, _onCharacterDie);
        }
        
        protected override void RemoveEvents()
        {
            this.RemoveListener(EventID.OnCharacterDie, _onCharacterDie);
        }
        
        public override void Open()
        {
            base.Open();
            
            _alive = LevelManager.Instance.TotalCharacter;

            SetAliveText(_alive);
            SetTutorial(true);
        }
        
        public void SetTutorial(bool isVisible)
        {
            tutorial.SetActive(isVisible);
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
