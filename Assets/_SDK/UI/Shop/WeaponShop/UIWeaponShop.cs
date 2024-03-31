using _Game.Scripts.Data;
using _Game.Scripts.Other.Utils;
using _Game.Scripts.Setting.Sound;
using _SDK.Observer.Scripts;
using _SDK.ServiceLocator.Scripts;
using _SDK.UI.Base;
using _SDK.UI.MainMenu;
using UnityEngine;

namespace _SDK.UI.Shop.WeaponShop
{
    public class UIWeaponShop : UICanvas
    {
        [SerializeField] private ItemWeaponShop itemPrefab;
        [SerializeField] private ButtonActionShop buttonActionShop;
       
        [SerializeField] private ItemShopDataSO itemShopData;
        
        private PlayerData PlayerData => this.GetService<DataManager>().PlayerData;
        
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
            
            buttonActionShop.OnSelectItem(itemPrefab);
            
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
            this.GetService<SoundManager>().Play(SoundType.ClickButton);
        }
        
        public void OnClickBackBtn()
        {
            _currentIndex--;
            if (_currentIndex < 0)
            {
                _currentIndex = itemShopData.Weapons.Count - 1;
            }
            
            InitItem(_currentIndex);
            this.GetService<SoundManager>().Play(SoundType.ClickButton);
        }
        
        public void OnClickCloseBtn()
        {
            CloseDirectly();
            UIManager.Ins.OpenUI<UIMainMenu>();
            this.PostEvent(EventID.OnCloseShop);
            this.GetService<SoundManager>().Play(SoundType.ClickButton);
        }
    }
}