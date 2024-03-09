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

        private void OnSelect()
        {
            SetSelection(true);

            this.PostEvent(EventID.OnSelectShopBar, this);
        }

        public void SetSelection(bool isSelect)
        {
            background.enabled = !isSelect;
        }
    }
}