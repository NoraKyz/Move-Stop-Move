using UnityEngine;

namespace _Game.Scripts.Weapon
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Data/Weapon")]
    public class WeaponData : ScriptableObject
    {
        public GameObject weaponPrefab;
    }
}
