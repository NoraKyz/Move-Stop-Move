using _Game.Scripts.GamePlay.Character.Player;
using _Game.Scripts.UI.Base;
using _Pattern.StateMachine.CharacterState;

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