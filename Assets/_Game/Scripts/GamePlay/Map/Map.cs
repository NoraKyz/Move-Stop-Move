using System.Collections.Generic;
using _Game.Scripts.GamePlay.Character;
using _Game.Scripts.Other.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts.GamePlay.Map
{
    public class Map : MonoBehaviour
    {
        private const float MIN_DISTANCE = 8f;
        
        [SerializeField] private float maxDistance;
        [SerializeField] private List<Transform> spawnPoints;
        
        private CharacterManager CharacterManager => CharacterManager.Ins;
        
#if UNITY_EDITOR
        private void Awake()
        {
            if (spawnPoints.Count < Constants.MAX_BOT_ON_MAP)
            {
                Debug.LogWarning("Not enough spawn points for bots");
            }
        }
#endif

        public Vector3 GetRandomPos()
        {
            return Utilities.GetRandomPosOnNavMesh(Vector3.zero, maxDistance);
        }
        
        public Vector3 GetRandomSpawnPos()
        {
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                if (!HasEnemyNear(spawnPoints[i].position))
                {
                    return spawnPoints[i].position;
                }
            }
            
            return spawnPoints[Random.Range(0, spawnPoints.Count)].position;
        }
        
        private bool HasEnemyNear(Vector3 pos)
        {
            bool isNear = IsNear(pos, CharacterManager.Player.TF.position);
            
            for (int i = 0; i < CharacterManager.ListBots.Count; i++)
            {
                if (IsNear(pos, CharacterManager.ListBots[i].TF.position))
                {
                    isNear = true;
                }
            }

            return isNear;
        }
        
        private bool IsNear(Vector3 pos1, Vector3 pos2)
        {
            return Vector3.Distance(pos1, pos2) < MIN_DISTANCE;
        }
    }
}