namespace _Game.Scripts.Other.Utils
{
    public class Constants
    {
        public const int TIME_TO_REVIVE = 5;
        public const float DEFAULT_ATTACK_RANGE = 5f;
        public const int MAX_BOT_ON_MAP = 8;
        public const int COIN_PER_GAME = 100;
    }
    
    public class AnimName
    {
        public const string IDLE = "Idle";
        public const string RUN = "Run";
        public const string ATTACK = "Attack";
        public const string ANIM_DIE = "Die";
        public const string WIN = "Win";
        public const string DANCE = "Dance";
    }
    
    public class TagName
    {
        public const string CHARACTER = "Character";
    }

    public enum HairType
    {
        None = 0,
        HArrow = 1,
        HCrown = 2,
        HEar = 3,
        HFlower = 4,
        HHair = 5,
        HHat = 6,
        HHatCap = 7,
        HHorn = 8,
        HRau = 9,
    }
    
    public enum PantType
    {
        None = 0,
        PBatman = 1,
        PChambi = 2,
        PComy = 3,
        PDabao = 4,
        POnion = 5,
        PPokemon = 6,
        PRainbow = 7,
        PSkull = 8,
        PVantim = 9,
    }
    
    public enum ShieldType
    {
        None = 0,
        ShShield1 = 1,
        ShShield2 = 2,
    }
    
    public enum WeaponType
    {
        WHammer = 0,
        WBoomerang = 1,
        WKnife = 2,
    }
    
    public enum SetSkinType
    {
        SDefault = 0,
        SDevil = 1,
        SAngel = 2,
        SWitch = 3,
        SDeadPool = 4,
        SThor = 5,
    }
    
    public enum ItemType
    {
        Hair = 0, 
        Pant = 1, 
        Shield = 2, 
        SetSkin = 3,
        Weapon = 4,
    }
    
    public enum ShopType
    {
        Weapon = ItemType.Weapon,
        Hair = ItemType.Hair,
        Pant = ItemType.Pant,
        Shield = ItemType.Shield,
        SetSkin = ItemType.SetSkin
    }
    
    public enum SlotType
    {
        Weapon = ItemType.Weapon,
        Hair = ItemType.Hair,
        Pant = ItemType.Pant,
        Shield = ItemType.Shield,
        SetSkin = ItemType.SetSkin,
    }
    
    public enum SettingState
    {
        Off = 0,
        On = 1
    }
}