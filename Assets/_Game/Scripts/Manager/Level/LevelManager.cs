using System.Collections.Generic;
using _Pattern.Singleton;
using UnityEngine;

namespace _Game.Scripts.Manager.Level
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private List<Level> levels = new List<Level>();
    }
}