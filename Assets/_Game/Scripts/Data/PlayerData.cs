using System;
using System.Collections.Generic;
using _Game.Scripts.Other.Utils;
using UnityEngine;

namespace _Game.Scripts.Data
{
    public static class KeyData
    {
        public const string Player = "player";
        public const string Coin = "coin";
        public const string Level = "level";
            
        public const string PlayerWeapon = "playerWeapon";
        public const string PlayerHair = "playerHair";
        public const string PlayerShield = "playerShield";
        public const string PlayerPant = "playerPant";
        public const string PlayerSetSkin = "playerSetSkin";
    }
    
    [Serializable]
    public class PlayerData
    {
        /// <summary>
        ///  0 = lock , 1 = unlock , 2 = equipped
        /// </summary>
        [Serializable]
        public struct Data
        {
            public string key;
            public int value;
        }
        
        [Header("--------- Game Setting ---------")]
        public bool isNew = true;

        //public bool isMusic = true;
        public bool isSound = true;
        public bool isVibrate = true;
        public bool isNoAds = false;
        public float volumeSound = 80f;

#if UNITY_EDITOR
        [Header("--------- Game Param Test ---------")]
        [SerializeField] private int coin;
        [SerializeField] private int level;
        
        [SerializeField] private int playerWeapon;
        [SerializeField] private int playerHair;
        [SerializeField] private int playerShield;
        [SerializeField] private int playerPant;
        [SerializeField] private int playerSetSkin;
#endif
        
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

        [SerializeField] private List<Data> listData = new ();
        
        private Dictionary<string, int> _data = new ();
        
        public void OnInit()
        {
            ConvertListDataToDictionary();
        }
        
#if UNITY_EDITOR
        public void LoadDataTest()
        {
            _data[KeyData.Coin] = coin;
            _data[KeyData.Level] = level;
            _data[KeyData.PlayerWeapon] = playerWeapon;
            _data[KeyData.PlayerHair] = playerHair;
            _data[KeyData.PlayerShield] = playerShield;
            _data[KeyData.PlayerPant] = playerPant;
            _data[KeyData.PlayerSetSkin] = playerSetSkin;
        }
#endif

        public int GetItemState(ItemType itemType, Enum enumId)
        {
            int id = Convert.ToInt32(enumId);
            string key = itemType.ToString() + id;
            return GetIntData(key);
        }
        
        public void SetItemState(ItemType itemType, Enum enumId, int state)
        {
            int id = Convert.ToInt32(enumId);
            string key = itemType.ToString() + id;
            _data[key] = state;
        }
        
        public int GetIntData(string key, int defaultValue = 0)
        {
            if (_data.TryGetValue(key, out var data))
            {
                return data;
            }

            _data[key] = defaultValue;
            return defaultValue;
        }
        
        public void SetIntData(string key, int value)
        {
            _data[key] = value;
        }
        
        public void OnEquipItem(ItemType itemType, Enum itemId, string whoEquip = KeyData.Player)
        {
            int id = Convert.ToInt32(itemId);
            SetIntData(whoEquip + itemType, id);
        }

        public void ConvertDictionaryToListData()
        {
            listData.Clear();
            
            foreach (var data in _data)
            {
                listData.Add(new Data {
                    key = data.Key,
                    value = data.Value
                });
            }
        }

        public void ConvertListDataToDictionary()
        {
            foreach (var data in listData)
            {
                _data[data.key] = data.value;
            }
        }
    }
}