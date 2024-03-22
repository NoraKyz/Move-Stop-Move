using _Game.Scripts.Other.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace _SDK.UI.Shop.SkinShop
{
    public class ItemSkin : ItemShop
    {
        #region Config

        [SerializeField] private Text iconEquipped;
        [SerializeField] private Image iconLock;
        [SerializeField] private Outline outline;
        [SerializeField] private Button button;
        
        public Button Button => button;
        
        #endregion

        public override void OnInit<T>(ItemType itemType, ItemShopData<T> itemData, State state)
        {
            base.OnInit(itemType, itemData, state);
            
            SetState(state);
            SetUIEquip();
            SetUISelection(false);
        }

        private void OnDisable()
        {
            OnDespawn();
        }

        public void OnDespawn()
        {
            button.onClick.RemoveAllListeners();
        }

        public void SetUIEquip()
        {
            iconEquipped.enabled = IsEquipped();
        }
        
        public void SetState(State state)
        {
            CurrentState = state;
            iconLock.enabled = state == State.Lock;
        }
        
        public void SetUISelection(bool isSelect)
        {
            outline.enabled = isSelect;
        }
    }
}