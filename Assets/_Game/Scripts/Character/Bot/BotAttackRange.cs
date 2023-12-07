using _Framework.Pool.Scripts;
using _Game.Utils;
using _Pattern;
using UnityEngine;

namespace _Game.Scripts.Character.Bot
{
    public class BotAttackRange : GameUnit
    {
        [SerializeField] private Character owner;

        private void OnEnable()
        {
            TF.localScale = Vector3.one * owner.AttackRange;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(TagName.Character))
            {
                Character enemy = Cache<Character>.GetComponent(other);

                if (enemy != owner)
                {
                    owner.OnEnemyEnterRange(enemy);
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(TagName.Character))
            {
                Character enemy = Cache<Character>.GetComponent(other);

                if (enemy != owner)
                {
                    owner.OnEnemyExitRange(enemy);
                }
            }
        }
    }
}