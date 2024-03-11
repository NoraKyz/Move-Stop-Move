using System;
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
        
        public Enum ItemType { get; private set; }

        public void OnInit<T>(SkinShopData<T> data, ItemShopState state) where T : Enum
        {
            ItemType = data.Type;
            
            imageItem.sprite = data.Sprite;
            button.onClick.AddListener(OnSelect);
            
            SetState(state); 
        }

        public void OnSelect()
        {
            SetUISelection(true);
            this.PostEvent(EventID.OnSelectSkinItem, this);
        }

        public void SetUISelection(bool isSelect)
        {
            outline.enabled = isSelect;
        }

        public void SetState(ItemShopState state)
        {
            iconEquipped.enabled = state == ItemShopState.Equipped;
            iconLock.enabled = state == ItemShopState.Lock;
        }
    }
}