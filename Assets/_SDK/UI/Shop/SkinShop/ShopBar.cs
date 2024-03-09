using System;
using _SDK.Observer.Scripts;
using UnityEngine;

namespace _SDK.UI.Shop.SkinShop
{
    public class ShopBar : MonoBehaviour
    {
        [SerializeField] private ButtonShopBar buttonDefaultSelected;
        
        private ButtonShopBar _currButtonSelected;
        
        private Action<object> _onSelectBar;

        public void OnInit()
        {
            buttonDefaultSelected.OnSelect();
        }

        private void OnEnable()
        {
            _onSelectBar = (param) => UpdateUIButtons((ButtonShopBar) param);
            this.RegisterListener(EventID.OnSelectShopBar, _onSelectBar);
        }

        private void OnDisable()
        {
            this.RemoveListener(EventID.OnSelectShopBar, _onSelectBar);
        }

        private void UpdateUIButtons(ButtonShopBar btn)
        { 
            if (_currButtonSelected != null)
            {
                _currButtonSelected.SetUISelection(false);
            }
            
            _currButtonSelected = btn;
        }
    }
}