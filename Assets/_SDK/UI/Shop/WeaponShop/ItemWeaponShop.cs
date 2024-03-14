using _Game.Scripts.Data;

namespace _SDK.UI.Shop.WeaponShop
{
    public class ItemWeaponShop : ItemShop
    {
        public override void OnEquip()
        {
            UserData.Ins.SetEnumData(UserData.Ins.PlayerWeapon.ToString(), State.Unlock);

            base.OnEquip();
        }
    }
}