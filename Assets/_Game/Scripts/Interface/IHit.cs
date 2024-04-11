using System;
using System.Security.Permissions;
using _Game.Scripts.GamePlay.Character.Base;

namespace _Game.Scripts.Interface
{
    public interface IHit
    {
        public bool IsDie { get; }  
        public void OnHit();
    }
}