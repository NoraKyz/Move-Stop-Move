using System;
using _SDK.Observer.Scripts;
using UnityEngine;

namespace _SDK.UI.Shop.SkinShop
{
    public class ShopBar : MonoBehaviour
    {
        [SerializeField] private ButtonShopBar buttonDefaultSelected;
        
        private ButtonShopBar _currentButtonSelected;
        
        private Action<object> _onSelectButtonBar;

        public void OnInit()
        {
            buttonDefaultSelected.OnSelect();
        }

        private void OnEnable()
        {
            _onSelectButtonBar = (param) => UpdateUIButtons((ButtonShopBar) param);
            this.RegisterListener(EventID.OnSelectShopBar, _onSelectButtonBar);
        }

        private void OnDisable()
        {
            this.RemoveListener(EventID.OnSelectShopBar, _onSelectButtonBar);
        }

        private void UpdateUIButtons(ButtonShopBar btn)
        { 
            if (_currentButtonSelected != null)
            {
                _currentButtonSelected.SetUISelection(false);
            }
            
            _currentButtonSelected = btn;
        }
    }
}