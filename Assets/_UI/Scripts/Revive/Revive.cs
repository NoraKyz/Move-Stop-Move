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
    }
}