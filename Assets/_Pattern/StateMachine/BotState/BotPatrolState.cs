using _Game.Scripts.Character.Bot;
using _Game.Scripts.Utils;
using _Game.Utils;
using UnityEngine;

namespace _Pattern.StateMachine.BotState
{
    public class BotPatrolState : IState<Bot>
    {
        private const float MaxDistance = 50f;
        public void OnEnter(Bot bot)
        {
            bot.ChangeAnim(AnimName.Run);
            
            Vector3 randomPos = Utilities.GetRandomPosOnNavMesh(Vector3.zero, MaxDistance);
            bot.MoveToPosition(randomPos);
        }

        public void OnExecute(Bot bot)
        {
            if (bot.IsDestination)
            {
                bot.ChangeState(new BotIdleState());
            }

            if (bot.HasEnemyInRange && Utilities.Chance(30))
            {
                bot.ChangeState(new BotAttackState());
            }
        }

        public void OnExit(Bot bot)
        {
            
        }
    }
}