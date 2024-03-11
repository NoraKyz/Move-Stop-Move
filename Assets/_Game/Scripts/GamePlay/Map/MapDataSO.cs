using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Map
{
    [CreateAssetMenu(fileName = "MapData", menuName = "Data/MapData")]
    public class MapDataSO : ScriptableObject
    {
        [SerializeField] private List<Map> maps;
        
        public Map GetMap(int id)
        {
            return maps[id];
        }
    }
}