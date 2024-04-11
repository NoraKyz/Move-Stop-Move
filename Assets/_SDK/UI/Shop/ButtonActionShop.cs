using System.Collections.Generic;
using _Game.Scripts.Data;
using _Game.Scripts.Setting.Sound;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
        [SerializeField] private Text textCost;
        
        private State _state;
        private ItemShop _currentItem;
        
        private PlayerData PlayerData => DataManager.Ins.PlayerData;
        
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
            
            if(state == State.Buy)
            {
                textCost.text = _currentItem.Cost.ToString();
            }
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
            
            SoundManager.Ins.Play(SoundType.ClickButton);
        }

        private void BuyItem()
        {
            if(PlayerData.Coin < _currentItem.Cost)
            {
                return;
            }

            PlayerData.Coin -= _currentItem.Cost;
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