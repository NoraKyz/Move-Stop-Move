using _SDK.Observer.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _SDK.UI.Shop.SkinShop
{
    public class ButtonBar : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private Button button;
        
        [SerializeField] private ShopType shopType;
        
        public ShopType ShopType => shopType;
        
        private void Awake()
        {
            button.onClick.AddListener(() => {
                this.PostEvent(EventID.OnSelectShopBar, shopType);
            });
        }

        public void SetState(bool isSelect)
        {
            background.enabled = !isSelect;
        }
    }
}