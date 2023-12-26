using _UI.Scripts.UI;
using UnityEngine;

namespace _UI.Scripts.Revive
{
    public class Revive : UICanvas
    {
        [SerializeField] private CountdownTimer timer;
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