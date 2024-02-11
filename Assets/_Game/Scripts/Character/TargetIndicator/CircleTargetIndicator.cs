using UnityEngine;

namespace _Game.Scripts.Character.TargetIndicator
{
    public class CircleTargetIndicator : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}