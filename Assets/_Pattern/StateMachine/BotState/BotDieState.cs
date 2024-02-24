using _Game.Scripts.GamePlay.Character.Bot;
using _Game.Scripts.Level;
using _Pattern.StateMachine.CharacterState;

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