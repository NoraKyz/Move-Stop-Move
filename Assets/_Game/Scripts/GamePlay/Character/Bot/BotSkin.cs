using _Game.Scripts.GamePlay.Character.Base;
using _Game.Scripts.Other.Utils;

namespace _Game.Scripts.GamePlay.Character.Bot
{
    public class BotSkin : CharacterSkin
    {
        public override void OnInit()
        {
            base.OnInit();
            
            ChangeWeapon(Utilities.RandomEnumValue<WeaponType>());
            ChangeShield(Utilities.RandomEnumValue<ShieldType>());
            ChangeHair(Utilities.RandomEnumValue<HairType>());
            ChangePant(Utilities.RandomEnumValue<PantType>());
        }
    }
}