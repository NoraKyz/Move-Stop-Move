using System;
using _Game.Scripts.GamePlay.Camera;
using _Game.Scripts.Level;
using _SDK.Observer.Scripts;
using _SDK.ServiceLocator.Scripts;
using _SDK.UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace _SDK.UI.GamePlay
{
    public class UIGamePlay : UICanvas
    {
        #region Config

        [Header("References")]
        [SerializeField] private Text aliveText;
        [SerializeField] private GameObject tutorial;
        
        private int _alive;
        
        private Action<object> _onCharacterDie;
        

        #endregion
        
        private void OnEnable()
        {
            _onCharacterDie = _ => OnCharacterDie();
            this.RegisterListener(EventID.OnCharacterDie, _onCharacterDie);
        }
        
        private void OnDisable()
        {
            this.RemoveListener(EventID.OnCharacterDie, _onCharacterDie);
        }
        
        public override void Open()
        {
            base.Open();
            
            this.GetService<CameraFollower>().ChangeState(CameraFollower.State.Gameplay);
            
            Level currentLevel = this.GetService<LevelManager>().CurrentLevel;
            _alive = currentLevel.TotalBots + 1;

            SetAliveText(_alive);
            SetTutorial(true);
        }
        
        private void OnCharacterDie()
        {
            _alive--;
            SetAliveText(_alive);
        }
        
        public void SetTutorial(bool isVisible)
        {
            tutorial.SetActive(isVisible);
        }
        
        private void SetAliveText(int alive)
        {
            aliveText.text = alive.ToString();
        }
    }
}
