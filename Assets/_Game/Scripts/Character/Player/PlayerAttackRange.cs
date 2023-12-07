using _Framework.Pool.Scripts;
using _Game.Utils;
using _Pattern;
using UnityEngine;

namespace _Game.Scripts.Character.Player
{
    public class PlayerAttackRange : GameUnit
    {
        [SerializeField] private Character owner;

        private void Start()
        {
            TF.localScale = Vector3.one * owner.AttackRange;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagName.Character))
            {
                Bot.Bot bot = Cache<Bot.Bot>.GetComponent(other);

                if (bot != null)
                {
                    bot.ShowCircleTargetIndicator();
                    owner.OnEnemyEnterRange(bot);
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
                    bot.HideCircleTargetIndicator();
                    owner.OnEnemyExitRange(bot);
                }
            }
        }
    }
}
