using _Pattern;
using UnityEngine;

namespace _Game.Scripts.Character.Player
{
    public class PlayerAttackRange : AttackRange
    {
        protected override void EnemyEnterRange(Collider other)
        {
            Bot.Bot bot = Cache<Bot.Bot>.GetComponent(other);

            if (bot != null)
            {
                bot.ShowCircleTargetIndicator();
                owner.OnEnemyEnterRange(bot);
            }
        }
        protected override void EnemyExitRange(Collider other)
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
