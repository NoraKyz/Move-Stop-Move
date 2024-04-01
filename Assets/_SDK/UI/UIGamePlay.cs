using System;
using _Game.Scripts.GamePlay.Camera;
using _Game.Scripts.GamePlay.Character;
using _Game.Scripts.Level;
using _SDK.Observer.Scripts;
using _SDK.ServiceLocator.Scripts;
using _SDK.UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace _SDK.UI
{
    public class UIGamePlay : UICanvas
    {
        #region Config

        [Header("References")]
        [SerializeField] private Text aliveText;
        [SerializeField] private GameObject tutorial;
        
        private int _alive;
        
        private Action<object> _onCharacterDie;
        private Action<object> _onPlayerRevive;
        
        public int Alive => _alive;

        #endregion
        
        private void OnEnable()
        {
            _onCharacterDie = _ => OnCharacterDie();
            this.RegisterListener(EventID.OnCharacterDie, _onCharacterDie);
            _onPlayerRevive = _ => OnPlayerRevive();
            this.RegisterListener(EventID.OnPlayerRevive, _onPlayerRevive);
        }
        
        private void OnDisable()
        {
            this.RemoveListener(EventID.OnCharacterDie, _onCharacterDie);
            this.RemoveListener(EventID.OnPlayerRevive, _onPlayerRevive);
        }
        
        public override void Open()
        {
            base.Open();
            
            this.GetService<CameraFollower>().ChangeState(CameraFollower.State.Gameplay);
            this.GetService<CharacterManager>().SetTargetIndicatorAlpha(1f);
            
            _alive = GetAlive();
            SetAliveText(_alive);
            SetTutorial(true);
        }

        public override void CloseDirectly()
        {
            base.CloseDirectly();
            
            this.GetService<CharacterManager>().SetTargetIndicatorAlpha(0f);
        }
        
        public void OnPlayerRevive()
        {
            _alive++;
            SetAliveText(_alive);
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

        private int GetAlive()
        {
            return this.GetService<LevelGameManager>().TotalBotsAlive + 1;
        }
        
        public void OnClickSetting()
        {
            UIManager.Ins.OpenUI<UISetting>();
            CloseDirectly();
        }
    }
}
