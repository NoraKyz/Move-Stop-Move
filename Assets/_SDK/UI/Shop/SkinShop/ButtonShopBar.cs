using _Game.Scripts.Other.Utils;
using _SDK.Observer.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _SDK.UI.Shop.SkinShop
{
    public class ButtonShopBar : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private Button button;
        
        [SerializeField] private ShopType shopType;
        
        public ShopType ShopType => shopType;

        private void Awake()
        {
            button.onClick.AddListener(OnSelect);
        }

        public void OnSelect()
        {
            this.PostEvent(EventID.OnSelectShopBar, this);
        }
        
        public void SetUISelection(bool isSelect)
        {
            background.enabled = !isSelect;
        }
    }
}