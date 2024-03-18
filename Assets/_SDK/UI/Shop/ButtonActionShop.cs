using System;
using System.Collections.Generic;
using _Game.Scripts.Data;
using _SDK.Observer.Scripts;
using UnityEngine;

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
        
        
        private State _state;
        private ItemShop _currentItem;
        private PlayerData PlayerData => DataManager.Ins.PlayerData;
        
        private Action<object> _onSelectOtherItem;

        #endregion
        
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
            }
        }

        private void BuyItem()
        {
            int currentCoin = PlayerData.GetIntData(KeyData.Coin);
            if(currentCoin < _currentItem.Cost)
            {
                return;
            }

            PlayerData.SetIntData(KeyData.Coin, currentCoin - _currentItem.Cost);
            this.PostEvent(EventID.OnChangeCoin);
            
            EquipItem();
        }

        private void EquipItem()
        {
            _currentItem.OnEquip();
            SetState((State) _currentItem.CurrentState);
        }
    }
}