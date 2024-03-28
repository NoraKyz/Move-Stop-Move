using System;
using _Game.Scripts.Data;
using _Game.Scripts.GamePlay.Character.Base;
using _Game.Scripts.Other.Utils;
using _SDK.Observer.Scripts;
using _SDK.ServiceLocator.Scripts;
using _SDK.UI.Shop;

namespace _Game.Scripts.GamePlay.Character.Player
{
    public class PlayerSkin : CharacterSkin
    {
        #region Config

        private PlayerData PlayerData => this.GetService<DataManager>().PlayerData;

        private Action<object> _onSelectSkinItem;

        #endregion

        private void OnEnable()
        {
            _onSelectSkinItem = (param) => TrySkin((ItemShop) param);
            this.RegisterListener(EventID.OnSelectItem, _onSelectSkinItem);
        }
        
        private void OnDisable()
        {
            this.RemoveListener(EventID.OnSelectItem, _onSelectSkinItem);
        }
        
        public override void OnInit(Base.Character character)
        {
            base.OnInit(character);
            
            ChangeWeapon((WeaponType) PlayerData.GetItemEquipped(ItemType.Weapon));
            ChangeHair((HairType) PlayerData.GetItemEquipped(ItemType.Hair));
            ChangeShield((ShieldType) PlayerData.GetItemEquipped(ItemType.Shield));
            ChangePant((PantType) PlayerData.GetItemEquipped(ItemType.Pant));
        }
        
        private void TrySkin(ItemShop item)
        {
            switch (item.ItemType)
            {
                case ItemType.Hair:
                    DespawnHair();
                    ChangeHair((HairType) item.Id);
                    break;
                case ItemType.Pant:
                    DespawnHair();
                    ChangePant((PantType) item.Id);
                    break;
                case ItemType.Shield:
                    DespawnShield();
                    ChangeShield((ShieldType) item.Id);
                    break;
                case ItemType.SetSkin:
                    DespawnWeapon();
                    Player player = (Player) owner;
                    player.SetSkin((SetSkinType) item.Id);
                    break;
                case ItemType.Weapon:
                    DespawnWeapon();
                    ChangeWeapon((WeaponType) item.Id);
                    break;
            }
        }
    }
}