using System.Collections.Generic;
using _Game.Scripts.Data;
using _SDK.Observer.Scripts;
using _SDK.ServiceLocator.Scripts;
using UnityEngine;
using UnityEngine.Events;

namespace _SDK.UI.Shop
{
    public class ButtonActionShop : MonoBehaviour
    {
        #region Config

        public enum State
        {
            Buy = 0,
            Equip = 1,
            Equipped = 2
        }
        
        [SerializeField] List<GameObject> stateViews;
        [SerializeField] private UnityEvent onReloadUIShop;
        
        private State _state;
        private ItemShop _currentItem;
        
        private PlayerData PlayerData => this.GetService<DataManager>().PlayerData;
        
        #endregion
        
        public void OnSelectItem(ItemShop item)
        {
            _currentItem = item;
            
            if (_currentItem.IsEquipped())
            {
                SetState(State.Equipped);
            }
            else
            {
                SetState((State) _currentItem.CurrentState);
            }
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
            }
        }

        private void BuyItem()
        {
            if(PlayerData.Coin < _currentItem.Cost)
            {
                return;
            }

            PlayerData.Coin -= _currentItem.Cost;
            this.PostEvent(EventID.OnChangeCoin);
            PlayerData.SetItemState(_currentItem.ItemType, _currentItem.Id, (int) ItemShop.State.Unlock);
            
            EquipItem();
        }

        private void EquipItem()
        {
            SetState(State.Equipped);
            PlayerData.SetItemEquipped(_currentItem.ItemType, _currentItem.Id);
            onReloadUIShop?.Invoke();
        }
    }
}