using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Weapon
{
    public enum WeaponType
    {
        Hammer = 0,
        Knight = 1,
        Boomerang = 2,
    }
    
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Data/Weapon")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private List<GameObject> weaponPrefabs = new List<GameObject>();
        public GameObject GetWeaponPrefab(WeaponType weaponType)
        {
            return weaponPrefabs[(int)weaponType];   
        }
    }
}
