using _Game.Scripts.Other.Utils;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Map
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private float maxDistance;
        
        public Vector3 GetRandomPos()
        {
            return Utilities.GetRandomPosOnNavMesh(Vector3.zero, maxDistance);
        }
    }
}