using _Game.Scripts.UI.Base;
using UnityEngine;

namespace _Game.Scripts.UI.Revive
{
    public class Revive : UICanvas
    {
        #region Config

        [Header("References")]
        [SerializeField] private CountdownTimer timer;

        #endregion
        
        public override void Open()
        {
            base.Open();
            
            timer.OnInit();
        }

        public void CloseBtn()
        {
            GameManager.ChangeState(GameState.Lose);
        }
    }
}