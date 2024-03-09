using System;
using _SDK.Observer.Scripts;
using UnityEngine;

namespace _SDK.UI.Shop.SkinShop
{
    public class ShopBar : MonoBehaviour
    {
        [SerializeField] private ButtonShopBar buttonShopBarDefaultSelected;
        
        private ButtonShopBar _currButtonSelected;
        
        private Action<object> _onSelectBar;

        private void OnEnable()
        {
            _onSelectBar = (param) => UpdateStateButtons((ButtonShopBar) param);
            this.RegisterListener(EventID.OnSelectShopBar, _onSelectBar);
        }

        private void OnDisable()
        {
            this.RemoveListener(EventID.OnSelectShopBar, _onSelectBar);
        }

        private void UpdateStateButtons(ButtonShopBar btn)
        { 
            if (_currButtonSelected != null)
            {
                _currButtonSelected.SetSelection(false);
            }
            
            _currButtonSelected = btn;
        }
    }
}