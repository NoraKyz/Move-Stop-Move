using _Game.Scripts.GamePlay.Camera;
using _Game.Scripts.GamePlay.Character;
using _Game.Scripts.GamePlay.Character.Base;
using _Game.Scripts.Level;
using _SDK.UI.Base;
using _SDK.UI.Revive;
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
        
        public int Alive => _alive;

        #endregion
        
        public override void Open()
        {
            base.Open();
            
            CameraFollower.Ins.ChangeState(CameraFollower.State.Gameplay);
            CharacterManager.Ins.SetTargetIndicatorAlpha(1f);
            
            _alive = GetAlive();
            SetAliveText(_alive);
            SetTutorial(true);
            
            Character.OnDeathAction += OnCharacterDie;
            UIRevive.OnPlayerRevive += OnPlayerRevive;
        }

        public override void CloseDirectly()
        {
            base.CloseDirectly();
            
            CharacterManager.Ins.SetTargetIndicatorAlpha(0f);
            Character.OnDeathAction -= OnCharacterDie;
            UIRevive.OnPlayerRevive -= OnPlayerRevive;
        }
        
        public void OnPlayerRevive()
        {
            _alive++;
            SetAliveText(_alive);
        }

        private void OnCharacterDie(Character character)
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
            return LevelGameManager.Ins.TotalBotsAlive + 1;
        }
        
        public void OnClickSetting()
        {
            UIManager.Ins.OpenUI<UISetting>();
            CloseDirectly();
        }
    }
}
