using _Game.Scripts.Character.Bot;

namespace _Pattern.StateMachine.BotState
{
    public class BotDieState : DieState<Bot>
    {
        public override void OnEnter(Bot bot)
        {
            base.OnEnter(bot);
            
            bot.StopMove();
        }
    }
}