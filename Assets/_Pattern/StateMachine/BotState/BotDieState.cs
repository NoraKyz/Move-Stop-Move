using _Game.Scripts.Character.Bot;
using _Game.Scripts.Manager.Level;

namespace _Pattern.StateMachine.BotState
{
    public class BotDieState : DieState<Bot>
    {
        public override void OnEnter(Bot bot)
        {
            base.OnEnter(bot);
            
            bot.StopMove();
        }

        protected override void Despawn(Bot bot)
        {
            base.Despawn(bot);
            
            LevelManager.Instance.BotDeath(bot);
        }
    }
}