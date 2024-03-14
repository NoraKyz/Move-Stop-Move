using System.Collections.Generic;
using _Game.Scripts.Other.Utils;
using UnityEngine;

namespace _SDK.UI.Shop
{
    [CreateAssetMenu(fileName = "ItemShopData", menuName = "Data/ItemShopData")]
    public class ItemShopDataSO : ScriptableObject
    {
        [SerializeField] private List<ItemShopData<WeaponType>> weapons;
        [SerializeField] private List<ItemShopData<HairType>> hairs;
        [SerializeField] private List<ItemShopData<PantType>> paints;
        [SerializeField] private List<ItemShopData<ShieldType>> shields;
        [SerializeField] private List<ItemShopData<SetType>> sets;
        
        public List<ItemShopData<WeaponType>> Weapons => weapons;
        public List<ItemShopData<HairType>> Hairs => hairs;
        public List<ItemShopData<PantType>> Paints => paints;
        public List<ItemShopData<ShieldType>> Shields => shields;
        public List<ItemShopData<SetType>> Sets => sets;
    }
}