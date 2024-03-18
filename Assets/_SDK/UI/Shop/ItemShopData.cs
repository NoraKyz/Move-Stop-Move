using System;
using UnityEngine;

namespace _SDK.UI.Shop
{
    [Serializable]
    public class ItemShopData<T> where T : Enum
    {
        [SerializeField] private T id;
        [SerializeField] private int cost;
        [SerializeField] private Sprite sprite;
        
        public T Id => id;
        public int Cost => cost;
        public Sprite Sprite => sprite;
    }
}