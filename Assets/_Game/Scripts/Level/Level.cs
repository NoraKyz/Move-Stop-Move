using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.Level
{
    [Serializable]
    public class Level : MonoBehaviour
    {
        [SerializeField] private int mapId;
        [SerializeField] private int totalBots;
        
        public int MapId => mapId;
        public int TotalBots => totalBots;
    }
}