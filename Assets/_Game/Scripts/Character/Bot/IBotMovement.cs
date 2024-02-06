using UnityEngine;

namespace _Game.Scripts.Character.Bot
{
    public interface IBotMovement
    {
        public bool IsDestination { get; }
        public void MoveToPosition(Vector3 position);
        public void StopMove();
    }
}