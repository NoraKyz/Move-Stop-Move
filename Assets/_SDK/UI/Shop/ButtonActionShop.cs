using System;
using System.Collections.Generic;
using _Game.Scripts.Data;
using _SDK.Observer.Scripts;
using UnityEngine;

namespace _SDK.UI.Shop
{
    
    
    public class ButtonActionShop : MonoBehaviour
    {
        [SerializeField] List<GameObject> stateViews;
        
        public enum State
        {
            Buy = 0,
            Equip = 1,
            Equipped = 2
        }
        
        private State _state;
        private ItemShop _currentItem;
        
        private Action<object> _onSelectOtherItem;
        
        private void OnEnable()
        {
            _onSelectOtherItem = (param) => OnSelectSkinItem((ItemShop) param);
            this.RegisterListener(EventID.OnSelectItem, _onSelectOtherItem);
        }

        private void OnDisable()
        {
            this.RegisterListener(EventID.OnSelectItem, _onSelectOtherItem);
        }

        private void OnSelectSkinItem(ItemShop item)
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
            int currentCoin = UserData.Ins.Coin;
            
            if(currentCoin < _currentItem.Cost)
            {
                return;
            }
            
            currentCoin -= _currentItem.Cost;
            UserData.Ins.SetCoin(currentCoin);
            this.PostEvent(EventID.OnChangeCoin, currentCoin);
            
            EquipItem();
        }

        private void EquipItem()
        {
            _currentItem.OnEquip();
            SetState((State) _currentItem.CurrentState);
        }
    }
}