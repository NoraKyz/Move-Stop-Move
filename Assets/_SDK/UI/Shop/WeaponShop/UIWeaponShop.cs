using System;
using _Game.Scripts.Data;
using _Game.Scripts.Other.Utils;
using _Game.Scripts.Setting.Sound;
using _SDK.UI.Base;
using _SDK.UI.MainMenu;
using UnityEngine;

namespace _SDK.UI.Shop.WeaponShop
{
    public class UIWeaponShop : UICanvas
    {
        public static event Action OnCloseWeaponShop;
        public static event Action<ItemShop> OnSelectedItemShopWeapon; 
        
        [SerializeField] private ItemWeaponShop itemPrefab;
        [SerializeField] private ButtonActionShop buttonActionShop;
       
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
            
            buttonActionShop.OnSelectItem(itemPrefab);
            
            OnSelectedItemShopWeapon?.Invoke(itemPrefab);
        }

        public void OnClickNextBtn()
        {
            _currentIndex++;
            if (_currentIndex >= itemShopData.Weapons.Count)
            {
                _currentIndex = 0;
            }
            
            InitItem(_currentIndex);
            SoundManager.Ins.Play(SoundType.ClickButton);
        }
        
        public void OnClickBackBtn()
        {
            _currentIndex--;
            if (_currentIndex < 0)
            {
                _currentIndex = itemShopData.Weapons.Count - 1;
            }
            
            InitItem(_currentIndex);
            SoundManager.Ins.Play(SoundType.ClickButton);
        }
        
        public void OnClickCloseBtn()
        {
            CloseDirectly();
            OnCloseWeaponShop?.Invoke();
            UIManager.Ins.OpenUI<UIMainMenu>();
            SoundManager.Ins.Play(SoundType.ClickButton);
        }
    }
}