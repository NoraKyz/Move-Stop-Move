using _Game.Scripts.Other.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace _SDK.UI.Shop.SkinShop
{
    public class ButtonBarSkin : MonoBehaviour
    {
        public enum ButtonType
        {
            SetSkin = ItemType.SetSkin,
            Hair = ItemType.Hair,
            Pant = ItemType.Pant,
            Shield = ItemType.Shield,
        }
        
        #region Config
        
        [SerializeField] private Button button;
        [SerializeField] private Image background;
        
        [SerializeField] private ButtonType buttonType;
        [SerializeField] private bool defaultSelect;
        
        public ButtonType Type => buttonType;
        public Button Button => button;
        public bool DefaultSelected => defaultSelect;
        
        #endregion

        public void SetUISelection(bool isSelect)
        {
            background.enabled = !isSelect;
        }
    }
}