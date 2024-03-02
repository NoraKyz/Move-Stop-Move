using _Game.Scripts.GamePlay.Character.Player;
using _SDK.StateMachine.CharacterState;
using _SDK.UI.Base;

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