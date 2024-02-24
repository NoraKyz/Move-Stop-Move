using UnityEngine;

namespace _Game.Scripts.Level
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData")]
    public class LevelDataSO : ScriptableObject
    {
        public float maxDistanceMap;
        public int totalCharacter;
        public GameObject mapPrefab;
    }
}