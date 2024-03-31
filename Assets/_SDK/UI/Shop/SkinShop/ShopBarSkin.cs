using System.Collections.Generic;
using _Game.Scripts.Other.Utils;
using _Game.Scripts.Setting.Sound;
using _SDK.ServiceLocator.Scripts;
using UnityEngine;

namespace _SDK.UI.Shop.SkinShop
{
    public class ShopBarSkin : MonoBehaviour
    {
        [SerializeField] private List<ButtonBarSkin> buttons;
        [SerializeField] private ShopSkin shopSkin;
        
        private void Awake()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                var button = buttons[i];
                button.Button.onClick.AddListener(() => OnSelectBar(button));
            }
        }

        public void OnInit()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].DefaultSelected)
                {
                    OnSelectBar(buttons[i]);
                }
            }
        }

        private void OnSelectBar(ButtonBarSkin button)
        {
            shopSkin.InitShop((ItemType) button.Type);
            ReloadUISelection(button.Type);
            this.GetService<SoundManager>().Play(SoundType.ClickButton);
        }

        private void ReloadUISelection(ButtonBarSkin.ButtonType buttonType)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].SetUISelection(buttons[i].Type == buttonType);
            }
        }
    }
}