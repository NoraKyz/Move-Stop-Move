using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.UI.Shop
{
    public enum ButtonShopState
    {
        Buy = 0,
        Equip = 1,
        Equipped = 2
    }
    
    public class ButtonShop : MonoBehaviour
    {
        [SerializeField] List<GameObject> stateViews;
        
        private ButtonShopState _state;
        
        public void SetState(ButtonShopState state)
        {
            _state = state;
            SetStateView(state);
        }
        
        private void SetStateView(ButtonShopState state)
        {
            stateViews.ForEach(view => view.SetActive(false));
            stateViews[(int) state].SetActive(true);
        }
        
        public void OnClick()
        {
            switch (_state)
            {
                case ButtonShopState.Buy:
                    // Buy
                    break;
                case ButtonShopState.Equip:
                    // Equip
                    break;
                case ButtonShopState.Equipped:
                    // Equipped
                    break;
            }
        }
    }
}