using _Game.Scripts.GamePlay.Character.Player;
using _Game.Scripts.UI.Base;
using _SDK.StateMachine.CharacterState;

namespace _SDK.StateMachine.PlayerState
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