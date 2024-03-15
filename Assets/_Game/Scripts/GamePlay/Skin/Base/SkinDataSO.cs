using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Skin.Base
{
    public class SkinDataSO <T> : ScriptableObject
    {
        [SerializeField] private List<T> prefabs = new List<T>();
        
        public T GetSkin(int index)
        {
            return prefabs[index];
        }
    }
}