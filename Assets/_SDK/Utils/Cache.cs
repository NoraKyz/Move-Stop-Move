﻿using System.Collections.Generic;
using UnityEngine;

namespace _SDK.Utils
{
    public static class Cache <T>
    {
        private static Dictionary<Collider, T> _dict = new Dictionary<Collider, T>();
        
        public static T GetComponent(Collider collider)
        {
            if (_dict.TryGetValue(collider, out var value))
            {
                return value;
            }
            
            
            T collectItems = collider.GetComponent<T>();
            _dict.Add(collider, collectItems);
            return collectItems;
        }
    }
}