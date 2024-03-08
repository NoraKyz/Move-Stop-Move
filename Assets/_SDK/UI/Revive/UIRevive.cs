using _SDK.UI.Base;
using UnityEngine;

namespace _SDK.UI.Revive
{
    public class UIRevive : UICanvas
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