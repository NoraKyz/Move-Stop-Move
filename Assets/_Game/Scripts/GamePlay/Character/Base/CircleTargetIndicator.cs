using UnityEngine;

namespace _Game.Scripts.GamePlay.Character.Base
{
    public class CircleTargetIndicator : MonoBehaviour
    {
        public void OnInit()
        {
            SetVisible(false);
        }
        
        public void SetVisible(bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }
    }
}