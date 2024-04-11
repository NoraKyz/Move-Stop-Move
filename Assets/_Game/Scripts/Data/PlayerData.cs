using System;
using System.Collections.Generic;
using _Game.Scripts.Other.Utils;
using Newtonsoft.Json;
using UnityEngine;

namespace _Game.Scripts.Data
{
    [Serializable]
    public class PlayerData
    {
        public static event Action<int> OnCoinChanged; 
        
        [Header("--------- Game Setting ---------")]
        //[SerializeField] private int isNew = 1;
        [SerializeField] private int isSound = 1;
        [SerializeField] private int isVibrate = 1;
        [SerializeField] private int isNoAds = 0;
        //[SerializeField] private float volumeSound = 80f;

        #region Getter/Setter Game Setting
        
        public int IsSound
        {
            get => isSound;
            set => isSound = value;
        }
        
        public int IsVibrate
        {
            get => isVibrate;
            set => isVibrate = value;
        }
        
        public int IsNoAds
        {
            get => isNoAds;
            set => isNoAds = value;
        }

        #endregion
        
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

        
        [Header("Game Params")]
        [SerializeField] private int level;
        [SerializeField] private int coin;
        
        #region Getter/Setter Game Params

        public int Level
        {
            get => level;
            set => level = value;
        }

        public int Coin
        {
            get => coin;
            set => SetCoin(value);
        }
        
        private void SetCoin(int value)
        {
            coin = value;
            OnCoinChanged?.Invoke(coin);
        }
        
        #endregion

        #region Equipment

        [JsonProperty]
        private Dictionary<SlotType, int> _equippedItems = new ();
        
        public int GetItemEquipped(ItemType itemType)
        {
            return _equippedItems.GetValueOrDefault((SlotType) itemType, 0);
        }

        public void SetItemEquipped(ItemType itemType, Enum enumId)
        {
            int id = Convert.ToInt32(enumId);
            _equippedItems[(SlotType)itemType] = id;
        }

        #endregion

        #region Shopping
        
        [JsonProperty]
        private Dictionary<ShopType, List<int>> _itemStates = new Dictionary<ShopType, List<int>>()
        {
            {ShopType.Weapon, new List<int>(){1,0,0}},
            {ShopType.Hair, new List<int>(){1,0,0,0,0,0,0,0,0,0}},
            {ShopType.Pant, new List<int>(){1,0,0,0,0,0,0,0,0,0}},
            {ShopType.Shield, new List<int>(){1,0,0}},
            {ShopType.SetSkin, new List<int>(){1,0,0,0,0,0}},
        };
        
        public int GetItemState(ItemType itemType, Enum itemIds)
        {
            int id = Convert.ToInt32(itemIds);
            return _itemStates[(ShopType)itemType][id];
        }
        
        /// <summary>
        /// state = 0: locked, state = 1: unlocked
        /// </summary>
        /// <param name="itemType"></param>
        /// <param name="itemIds"></param>
        /// <param name="state"></param>
        public void SetItemState(ItemType itemType, Enum itemIds, int state)
        {
            int id = Convert.ToInt32(itemIds);
            _itemStates[(ShopType)itemType][id] = state;
        }

        #endregion
    }
}