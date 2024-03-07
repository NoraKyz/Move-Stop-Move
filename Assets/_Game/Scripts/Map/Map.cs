using System.Collections.Generic;
using _Game.Scripts.Other.Utils;
using UnityEngine;

namespace _Game.Scripts.Map
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private List<Transform> spawnPos;
            
        [SerializeField] private float maxDistance;
        
        public Vector3 GetRandomPos()
        {
            return Utilities.GetRandomPosOnNavMesh(Vector3.zero, maxDistance);
        }

        public Vector3 GetRandomSpawnPos()
        {
            return spawnPos[Random.Range(0, spawnPos.Count)].position;
        }
    }
}