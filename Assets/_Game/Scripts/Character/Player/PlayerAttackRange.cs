using System;
using _Framework;
using _Game.Scripts.Utils;
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
                bot.ShowTargetCircle();
                onEnemyEnterRange?.Invoke(bot);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(TagName.Character))
            {
                Bot.Bot bot = Cache<Bot.Bot>.GetComponent(other);
                bot.HideTargetCricle();
                onEnemyExitRange?.Invoke(bot);
            }
        }
    }
}
