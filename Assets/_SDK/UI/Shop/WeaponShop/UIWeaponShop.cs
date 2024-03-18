using _Game.Scripts.Data;
using _Game.Scripts.Other.Utils;
using _SDK.Observer.Scripts;
using _SDK.UI.Base;
using UnityEngine;

namespace _SDK.UI.Shop.WeaponShop
{
    public class UIWeaponShop : UICanvas
    {
        [SerializeField] private ItemWeaponShop itemPrefab;
       
        [SerializeField] private ItemShopDataSO itemShopData;
        
        private PlayerData PlayerData => DataManager.Ins.PlayerData;
        
        private int _currentIndex;

        public override void Open()
        {
            base.Open();
            
            _currentIndex = 0;
            InitItem(_currentIndex);
        }

        private void InitItem(int id)
        {
            ItemShop.State state = (ItemShop.State) PlayerData.GetItemState(ItemType.Weapon, itemShopData.Weapons[id].Id);
            itemPrefab.OnInit(ItemType.Weapon, itemShopData.Weapons[id], state);
            
            this.PostEvent(EventID.OnSelectItem, itemPrefab);
        }

        public void OnClickNextBtn()
        {
            _currentIndex++;
            if (_currentIndex >= itemShopData.Weapons.Count)
            {
                _currentIndex = 0;
            }
            InitItem(_currentIndex);
        }
        
        public void OnClickBackBtn()
        {
            _currentIndex--;
            if (_currentIndex < 0)
            {
                _currentIndex = itemShopData.Weapons.Count - 1;
            }
            InitItem(_currentIndex);
        }
        
        public void OnClickCloseBtn()
        {
            CloseDirectly();
            this.PostEvent(EventID.OnCloseShop);
            UIManager.Ins.OpenUI<UIMainMenu>();
        }
    }
}