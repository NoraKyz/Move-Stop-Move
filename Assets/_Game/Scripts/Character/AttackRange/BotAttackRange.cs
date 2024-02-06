using _Pattern;
using UnityEngine;

namespace _Game.Scripts.Character.Bot
{
    public class BotAttackRange : AttackRange
    {
        protected override void EnemyEnterRange(Collider other)
        {
            Character enemy = Cache<Character>.GetComponent(other);

            if (enemy != owner)
            {
                owner.OnEnemyEnterRange(enemy);
            }
        }
        protected override void EnemyExitRange(Collider other)
        {
            Character enemy = Cache<Character>.GetComponent(other);

            if (enemy != owner)
            {
                owner.OnEnemyExitRange(enemy);
            }
        }
    }
}