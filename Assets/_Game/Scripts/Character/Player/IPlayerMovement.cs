namespace _Game.Scripts.Character.Player
{
    public interface IPlayerMovement
    {
        public bool IsMoving { get; }
        public void Move();
    }
}