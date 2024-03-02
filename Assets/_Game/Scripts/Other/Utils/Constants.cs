namespace _Game.Scripts.Other.Utils
{
    public static class Constants
    {
        public const int TimeToRevive = 5;
        
        public const float DefaultAttackRange = 5f;
        
        public const int MaxBotOnMap = 8;
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
        Arrow = 1,
        Crown = 2,
        Ear = 3,
        Flower = 4,
        Hair = 5,
        Hat = 6,
        HatCap = 7,
        Horn = 8,
        Rau = 9,
    }
    
    public enum PantType
    {
        None = 0,
        Batman = 1,
        Chambi = 2,
        Comy = 3,
        Dabao = 4,
        Onion = 5,
        Pokemon = 6,
        Rainbow = 7,
        Skull = 8,
        Vantim = 9,
    }
    
    public enum ShieldType
    {
        None = 0,
        Shield1 = 1,
        Shield2 = 2,
    }
    
    public enum WeaponType
    {
        Arrow = 0,
        Axe1 = 1,
        Axe2 = 2,
        Boomerang = 3,
        Candy0 = 4,
        Candy1 = 5,
        Candy2 = 6,
        Candy4 = 7,
        Hammer = 8,
        Knife = 9,
        Uzi = 10,
        Z = 11,
    }
}