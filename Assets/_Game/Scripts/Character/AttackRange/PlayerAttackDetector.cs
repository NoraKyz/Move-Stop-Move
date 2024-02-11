using _Pattern;
using UnityEngine;

namespace _Game.Scripts.Character.AttackRange
{
    public class PlayerAttackDetector : AttackDetector
    {
        protected override void CharacterEnterRange(Collider other)
        {
            Bot.Bot bot = Cache<Bot.Bot>.GetComponent(other);

            if (IsEnemy(bot))
            {
                OnEnemyEnterRange(bot);
                bot.ShowCircleTargetIndicator();
            }
        }
        protected override void CharacterExitRange(Collider other)
        {
            Bot.Bot bot = Cache<Bot.Bot>.GetComponent(other);

            if (IsEnemy(bot))
            {
                OnEnemyExitRange(bot);
                bot.HideCircleTargetIndicator();
            }
        }
    }
}
