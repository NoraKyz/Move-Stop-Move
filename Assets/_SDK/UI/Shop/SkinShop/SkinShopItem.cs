using System;
using _SDK.Observer.Message;
using _SDK.Observer.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace _SDK.UI.Shop.SkinShop
{
    public enum ItemShopState
    {
        Lock = 0,
        Unlock = 1,
        Equipped = 2
    }
    
    public class SkinShopItem : MonoBehaviour
    {
        [SerializeField] private Image imageItem;
        [SerializeField] private Text iconEquipped;
        [SerializeField] private Image iconLock;
        [SerializeField] private Outline outline;
        [SerializeField] private Button button;

        private ItemShopState _currentState;

        public void OnInit<T>(SkinShopData<T> data, ItemShopState state) where T : Enum
        {
            imageItem.sprite = data.Sprite;
            button.onClick.AddListener(() => OnSelect(data));
            SetState(state); 
        }

        public void OnSelect<T> (SkinShopData<T> data) where T : Enum
        {
            SetUISelection(true);

            ItemSelectedMessage<T> mess = new ItemSelectedMessage<T>(data, this);
            this.PostEvent(EventID.OnSelectSkinItem, mess);
        }

        public void SetUISelection(bool isSelect)
        {
            outline.enabled = isSelect;
        }

        public void SetState(ItemShopState state)
        {
            _currentState = state;

            iconEquipped.enabled = state == ItemShopState.Equipped;
            iconLock.enabled = state == ItemShopState.Lock;
        }
    }
}