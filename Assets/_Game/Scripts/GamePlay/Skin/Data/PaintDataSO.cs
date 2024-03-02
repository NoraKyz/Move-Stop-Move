using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Skin.Base
{
    [CreateAssetMenu(fileName = "PaintData", menuName = "Data/PaintData")]
    public class PaintDataSO : ScriptableObject
    {
        [SerializeField] private List<Material> materials = new List<Material>();
        
        public Material GetMaterial(int index)
        {
            return materials[index];
        }
    }
}