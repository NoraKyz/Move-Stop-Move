using System;
using _Game.Scripts.Other.Utils;
using _SDK.UI.Shop;
using UnityEngine;

namespace _Game.Scripts.Data
{
    [Serializable]
    public class PlayerData
    {
        [Header("--------- Game Setting ---------")]
        public bool isNew = true;

        //public bool isMusic = true;
        public bool isSound = true;
        public bool isVibrate = true;
        public bool isNoAds = false;
        public float volumeSound = 80f;
        

        [Header("--------- Game Params ---------")]
        public int coin = 0;
        
        public int level = 0; //Level hiện tại
        
        // TODO: getter, setter
        public int playerWeapon = 0;
        public int playerHair = 0;
        public int playerPant = 0;
        public int playerShield = 0;
        public int playerSet = 0;

        /// <summary>
        ///  0 = lock , 1 = unlock , 2 = equipped
        /// </summary>
        
        public int[] weaponStatus = new int[]
        {
           2, 0, 0
        };
        
        public int[] hairStatus = new int[]
        {
            2, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };
        
        public int[] pantStatus = new int[]
        {
            2, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };
        
        public int[] shieldStatus = new int[]
        {
            2, 0, 0
        };
        
        public int[] setStatus = new int[]
        {
            2, 0, 0, 0, 0, 0
        };


        // [Header("--------- Firebase ---------")]
        // public string timeInstall; //Thời điểm cài game
        //
        // public int timeLastOpen; //Thời điểm cuối cùng mở game. Tính số ngày kể từ 1/1/1970
        // public int timeInstallForFirebase; //Dùng trong hàm bắn Firebase UserProperty. Số ngày tính từ ngày 1/1/1970
        // public int daysPlayed = 0; //Số ngày đã User có mở game lên
        // public int sessionCount = 0; //Tống số session
        // public int playTime = 0; //Tổng số lần nhấn play game
        // public int playTimeSession = 0; //Số lần nhấn play game trong 1 session
        // public int dieCountLevelCur = 0; //Số lần chết tại level hiện tại
        // public int firstDayLevelPlayed = 0; //Số level đã chơi ở ngày đầu tiên

        //--------- Others ---------

        // public int rw_watched = 0; // Số lần xem quảng cáo reward
        // public int inter_watched = 0; // Số lần xem quảng cáo interstitial
        // public int level_played_1st_day = 0; // Số level đã chơi ở ngày đầu tiên

        public void EquipItemShop(ItemShop item)
        {
            int idItem = Convert.ToInt32(item.ItemType);
            
            switch (item.ShopType)
            {
                case ShopType.Weapon:
                    playerWeapon = idItem;
                    break;
                case ShopType.Hair:
                    playerHair = idItem;
                    break;
                case ShopType.Pant:
                    playerPant = idItem;
                    break;
                case ShopType.Shield:
                    playerShield = idItem;
                    break;
                case ShopType.Set:
                    playerSet = idItem;
                    break;
            }
        }
        
        public int GetItemState(ShopType shopType, Enum type)
        {
            int idItem = Convert.ToInt32(type);

            switch (shopType)
            {
                case ShopType.Weapon:
                    return weaponStatus[idItem];
                case ShopType.Hair:
                    return hairStatus[idItem];
                case ShopType.Pant:
                    return pantStatus[idItem];
                case ShopType.Shield:
                    return shieldStatus[idItem];
                case ShopType.Set:
                    return setStatus[idItem];
            }

            return 0;
        }

        public void SetItemState(ItemShop item, int state)
        {
            int idItem = Convert.ToInt32(item.ItemType);
            
            switch (item.ShopType)
            {
                case ShopType.Weapon:
                    weaponStatus[idItem] = state;
                    break;
                case ShopType.Hair:
                    hairStatus[idItem] = state;
                    break;
                case ShopType.Pant:
                    pantStatus[idItem] = state;
                    break;
                case ShopType.Shield:
                    shieldStatus[idItem] = state;
                    break;
                case ShopType.Set:
                    setStatus[idItem] = state;
                    break;
            }
        }
    }
}