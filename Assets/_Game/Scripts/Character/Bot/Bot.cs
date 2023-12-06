using UnityEngine;

namespace _Game.Scripts.Character.Bot
{
    public class Bot : Character
    {
        [Header("Components")]
        [SerializeField] private GameObject targetIndicator;
        public void ShowTargetIndicator()
        {
            targetIndicator.SetActive(true);
        }

        public void HideTargetIndicator()
        {
            targetIndicator.SetActive(false);
        }
    }
}
