using _Game.Utils;
using _Pattern;
using UnityEngine;
using UnityEngine.Events;

namespace _Game.Scripts.Character.Player
{
    public class PlayerAttackRange : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Bot.Bot> onEnemyEnterRange;
        [SerializeField] private UnityEvent<Bot.Bot> onEnemyExitRange;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagName.Character))
            {
                Bot.Bot bot = Cache<Bot.Bot>.GetComponent(other);

                if (bot != null)
                {
                    bot.ShowTargetIndicator();
                    onEnemyEnterRange?.Invoke(bot);
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(TagName.Character))
            {
                Bot.Bot bot = Cache<Bot.Bot>.GetComponent(other);

                if (bot != null)
                {
                    bot.HideTargetIndicator();
                    onEnemyExitRange?.Invoke(bot);
                }
            }
        }
    }
}
