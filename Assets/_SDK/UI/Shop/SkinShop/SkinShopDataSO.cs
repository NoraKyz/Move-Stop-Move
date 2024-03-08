using System.Collections.Generic;
using _Game.Scripts.Other.Utils;
using UnityEngine;

namespace _SDK.UI.Shop.SkinShop
{
    [CreateAssetMenu(fileName = "SkinShopData", menuName = "Data/SkinShopData")]
    public class SkinShopDataSO : ScriptableObject
    {
        [SerializeField] private List<SkinShopData<HairType>> hairs;
        [SerializeField] private List<SkinShopData<PantType>> paints;
        [SerializeField] private List<SkinShopData<ShieldType>> shields;
        [SerializeField] private List<SkinShopData<SetType>> sets;
        
        public List<SkinShopData<HairType>> Hairs => hairs;
        public List<SkinShopData<PantType>> Paints => paints;
        public List<SkinShopData<ShieldType>> Shields => shields;
        public List<SkinShopData<SetType>> Sets => sets;
    }
}