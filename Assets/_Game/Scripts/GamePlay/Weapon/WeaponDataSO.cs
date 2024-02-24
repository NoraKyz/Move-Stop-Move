using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Weapon
{
    public enum WeaponType
    {
        Hammer = 0,
        Knight = 1,
        Boomerang = 2,
    }
    
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Data/Weapon")]
    public class WeaponDataSO : ScriptableObject
    {
        [SerializeField] private List<GameObject> weaponPrefabs = new List<GameObject>();
        
        public GameObject GetWeaponPrefab(WeaponType weaponType)
        {
            return weaponPrefabs[(int)weaponType];   
        }
    }
}
