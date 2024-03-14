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
        
        private int _currentIndex;

        public override void Open()
        {
            base.Open();
            
            _currentIndex = 0;
            InitItem(_currentIndex);
        }

        private void InitItem(int id)
        {
            ItemShop.State state = UserData.Ins.GetEnumData(itemShopData.Weapons[id].Type.ToString(), ItemShop.State.Lock);
            itemPrefab.OnInit(ShopType.Weapon, itemShopData.Weapons[id], state);
            
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
            UIManager.Instance.OpenUI<UIMainMenu>();
        }
    }
}