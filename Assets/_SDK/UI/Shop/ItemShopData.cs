using System;
using UnityEngine;

namespace _SDK.UI.Shop
{
    [Serializable]
    public class ItemShopData<T> where T : Enum
    {
        [SerializeField] private T type;
        [SerializeField] private int cost;
        [SerializeField] private Sprite sprite;
        
        public T Type => type;
        public int Cost => cost;
        public Sprite Sprite => sprite;
    }
}