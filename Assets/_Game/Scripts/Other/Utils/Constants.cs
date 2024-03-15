namespace _Game.Scripts.Other.Utils
{
    public static class Constants
    {
        public const int TimeToRevive = 5;
        
        public const float DefaultAttackRange = 5f;
        
        public const int MaxBotOnMap = 8;
        
        public const float TimeDespawnBullet = 2f;
    }
    
    public static class AnimName
    {
        public const string Idle = "Idle";
        public const string Run = "Run";
        public const string Attack = "Attack";
        public const string Die = "Die";
        public const string Win = "Win";
        public const string Dance = "Dance";
    }
    
    public static class TagName
    {
        public const string Character = "Character";
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
    
    public enum SetType
    {
        SDefault = 0,
        SDevil = 1,
        SAngel = 2,
        SWitch = 3,
        SDeadPool = 4,
        SThor = 5,
    }
    
    public enum ShopType
    {
        Hair = 0, 
        Pant = 1, 
        Shield = 2, 
        Set = 3,
        Weapon = 4,
    }
}