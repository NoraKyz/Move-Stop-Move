using UnityEngine;

namespace _Game.Scripts.Manager.Level
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private float maxDistanceMap;
        [SerializeField] private int totalBot;
        
        public float MaxDistanceMap => maxDistanceMap;
        public int TotalBot => totalBot;
    }
}