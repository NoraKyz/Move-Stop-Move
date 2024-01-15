using _Game.Scripts.Character.Player;
using _UI.Scripts.UI;

namespace _Pattern.StateMachine.PlayerState
{
    public class PlayerDieState : DieState<Player>
    {
        protected override void Despawn(Player player)
        {
            base.Despawn(player);
            
            GameManager.ChangeState(GameState.Revive);
        }
    }
}