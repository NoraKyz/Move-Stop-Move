using System;
using System.Collections.Generic;
using _Game.Scripts.Data;
using _SDK.Observer.Scripts;
using _SDK.UI.Shop.SkinShop;
using UnityEngine;

namespace _SDK.UI.Shop
{
    
    
    public class ButtonShop : MonoBehaviour
    {
        [SerializeField] List<GameObject> stateViews;
        
        public enum State
        {
            Buy = 0,
            Equip = 1,
            Equipped = 2
        }
        
        private State _state;
        private SkinShopItem _currentItem;
        
        private Action<object> _onSelectSkinItem;
        private Action<object> _onEquipSkinItem;

        private void OnEnable()
        {
            _onSelectSkinItem = (param) => OnSelectSkinItem((SkinShopItem) param);
            this.RegisterListener(EventID.OnSelectSkinItem, _onSelectSkinItem);
        }

        private void OnDisable()
        {
            this.RegisterListener(EventID.OnSelectSkinItem, _onSelectSkinItem);
        }

        private void OnSelectSkinItem(SkinShopItem item)
        {
            _currentItem = item;
            SetState((State) item.CurrentState);
        }
        
        private void SetState(State state)
        {
            _state = state;
            SetUIState(state);
        }
        
        private void SetUIState(State state)
        {
            stateViews.ForEach(view => view.SetActive(false));
            stateViews[(int) state].SetActive(true);
        }
        
        public void OnClick()
        {
            switch (_state)
            {
                case State.Buy:
                    BuyItem();
                    break;
                case State.Equip:
                    EquipItem();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void BuyItem()
        {
            EquipItem();
        }

        private void EquipItem()
        {
            _currentItem.OnEquip();
            SetState((State) _currentItem.CurrentState);
        }
    }
}