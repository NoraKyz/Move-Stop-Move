using System;
using UnityEngine;

namespace _Game.Scripts.Level
{
    [Serializable]
    public class Level
    {
        [SerializeField] private int mapId;
        [SerializeField] private int totalBots;
        
        public int MapId => mapId;
        public int TotalBots => totalBots;
    }
}