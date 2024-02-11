using _Pattern;
using UnityEngine;

namespace _Game.Scripts.Character.AttackRange
{
    public class BotAttackDetector : AttackDetector
    {
        protected override void CharacterEnterRange(Collider other)
        {
            Character character = Cache<Character>.GetComponent(other);

            if (IsEnemy(character))
            {
                OnEnemyEnterRange(character);
            }
        }
        protected override void CharacterExitRange(Collider other)
        {
            Character character = Cache<Character>.GetComponent(other);

            if (IsEnemy(character))
            {
                OnEnemyExitRange(character);
            }
        }
    }
}