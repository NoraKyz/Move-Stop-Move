using System;
using _Game.Scripts.Other.Utils;
using _SDK.Observer.Scripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _SDK.UI.Shop.SkinShop
{
    public class ButtonShopBar : MonoBehaviour
    {
        #region Config

        [SerializeField] private Image background;
        [SerializeField] private Button button;
        
        [FormerlySerializedAs("shopType")] [SerializeField] private ItemType itemType;
        [SerializeField] private bool defaultSelect;
        
        private Action<object> _onSelectShopBar;
        
        public ItemType ItemType => itemType;

        #endregion

        private void Awake()
        {
            button.onClick.AddListener(OnSelect);
        }

        private void OnEnable()
        {
            _onSelectShopBar = (param) => SetUISelection((ButtonShopBar) param == this);
            this.RegisterListener(EventID.OnSelectShopBar, _onSelectShopBar);
            
            OnInit();
        }

        private void OnDisable()
        {
            this.RemoveListener(EventID.OnSelectShopBar, _onSelectShopBar);
        }

        private void OnInit()
        {
            SetUISelection(false);

            if (defaultSelect)
            {
                OnSelect();
            }
        }

        private void OnSelect()
        {
            this.PostEvent(EventID.OnSelectShopBar, this);
        }
        
        private void SetUISelection(bool isSelect)
        {
            background.enabled = !isSelect;
        }
    }
}