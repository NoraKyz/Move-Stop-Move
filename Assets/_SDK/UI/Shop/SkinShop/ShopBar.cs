using System;
using System.Collections.Generic;
using _SDK.Observer.Scripts;
using UnityEngine;

namespace _SDK.UI.Shop.SkinShop
{
    public class ShopBar : MonoBehaviour
    {
        [SerializeField] private List<ButtonBar> buttons;
        
        private Action<object> _onSelectBar;

        private void OnEnable()
        {
            _onSelectBar = (param) => UpdateStateButtons((ShopType) param);
            this.RegisterListener(EventID.OnSelectShopBar, _onSelectBar);
        }

        private void OnDisable()
        {
            this.RemoveListener(EventID.OnSelectShopBar, _onSelectBar);
        }

        private void UpdateStateButtons(ShopType shopType)
        { 
            for(int i = 0; i < buttons.Count; i++)
            {
                ShopType type = buttons[i].ShopType;
                buttons[i].SetState(type == shopType);
            }
        }
    }
}