using _Game.Scripts.Data;
using _Game.Scripts.GamePlay.Character.Base;
using _Game.Scripts.Other.Utils;
using _SDK.UI.Shop;
using _SDK.UI.Shop.SkinShop;
using _SDK.UI.Shop.WeaponShop;

namespace _Game.Scripts.GamePlay.Character.Player
{
    public class PlayerSkin : CharacterSkin
    {
        #region Config

        private PlayerData PlayerData => DataManager.Ins.PlayerData;
        
        #endregion

        public void Awake()
        {
            ShopSkin.OnSelectedItemShopSkin += TrySkin;
            UIWeaponShop.OnSelectedItemShopWeapon += TrySkin;
        }

        public void OnDestroy()
        {
            ShopSkin.OnSelectedItemShopSkin -= TrySkin;
            UIWeaponShop.OnSelectedItemShopWeapon -= TrySkin;
        }

        public override void OnInit(Base.Character character)
        {
            base.OnInit(character);
            
            ChangeWeapon((WeaponType) PlayerData.GetItemEquipped(ItemType.Weapon));
            ChangeHair((HairType) PlayerData.GetItemEquipped(ItemType.Hair));
            ChangeShield((ShieldType) PlayerData.GetItemEquipped(ItemType.Shield));
            ChangePant((PantType) PlayerData.GetItemEquipped(ItemType.Pant));
        }
        
        public void TrySkin(ItemShop item)
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