using System;
using _Game.Scripts.GamePlay.Character.Base;

namespace _Game.Scripts.Interface
{
    public interface IHit
    {
        public void OnHit(Action hitAction, Character killer);
    }
}