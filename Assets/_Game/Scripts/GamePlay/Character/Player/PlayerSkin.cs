using System;
using _Game.Scripts.Data;
using _Game.Scripts.GamePlay.Character.Base;
using _Game.Scripts.Other.Utils;
using _SDK.Observer.Scripts;
using _SDK.UI.Shop;

namespace _Game.Scripts.GamePlay.Character.Player
{
    public class PlayerSkin : CharacterSkin
    {
        #region Config

        private PlayerData PlayerData => DataManager.Ins.PlayerData;

        private Action<object> _onSelectSkinItem;
        private Action<object> _onCloseSkinShop;

        #endregion

        private void OnEnable()
        {
            _onSelectSkinItem = (param) => TrySkin((ItemShop) param);
            this.RegisterListener(EventID.OnSelectItem, _onSelectSkinItem);

            _onCloseSkinShop = (_) => OnInit();
            this.RegisterListener(EventID.OnCloseShop, _onCloseSkinShop);
        }
        
        private void OnDisable()
        {
            this.RemoveListener(EventID.OnSelectItem, _onSelectSkinItem);
            this.RemoveListener(EventID.OnCloseShop, _onCloseSkinShop);
        }
        
        public override void OnInit()
        {
            base.OnInit();
            
            ChangeWeapon((WeaponType) PlayerData.GetIntData(KeyData.PlayerWeapon));
            ChangeHair((HairType) PlayerData.GetIntData(KeyData.PlayerHair));
            ChangeShield((ShieldType) PlayerData.GetIntData(KeyData.PlayerShield));
            ChangePant((PantType) PlayerData.GetIntData(KeyData.PlayerPant));
        }

        private void TrySkin(ItemShop item)
        {
            switch (item.Type)
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
                case ItemType.Set:
                    break;
                case ItemType.Weapon:
                    DespawnWeapon();
                    ChangeWeapon((WeaponType) item.Id);
                    break;
            }
        }
    }
}