using System;
using System.Collections.Generic;
using _Game.Scripts.Other.Utils;
using UnityEngine;

namespace _Game.Scripts.Data
{
    public enum KeyData
    {
        Coin = 0,
        Level = 1,
        PlayerWeapon = 2,
        PlayerHair = 3,
        PlayerShield = 4,
        PlayerPant = 5,
        PlayerSetSkin = 6
    }

    [Serializable]
    public class PlayerData
    {
        [Serializable]
        public struct Data
        {
            public KeyData key;
            // Kiểu object nếu muốn lưu nhiều kiểu dữ liệu
            public int value; 
        }

        [Header("--------- Game Setting ---------")]
        [SerializeField] private bool isNew = true;
        [SerializeField] private bool isSound = true;
        [SerializeField] private bool isVibrate = true;
        [SerializeField] private bool isNoAds = false;
        [SerializeField] private float volumeSound = 80f;

        #region Getter/Setter Game Setting

        public bool IsNew
        {
            get => isNew;
            set => isNew = value;
        }
        
        public bool IsSound
        {
            get => isSound;
            set => isSound = value;
        }
        
        public bool IsVibrate
        {
            get => isVibrate;
            set => isVibrate = value;
        }
        
        public bool IsNoAds
        {
            get => isNoAds;
            set => isNoAds = value;
        }
        
        public float VolumeSound
        {
            get => volumeSound;
            set => volumeSound = value;
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

#if UNITY_EDITOR
        [Header("--------- Game Param Test ---------")]
        [SerializeField] private int coin;
        [SerializeField] private int level;
#endif

        // To save data in json
        [SerializeField] private List<Data> listData = new ();

        private Dictionary<KeyData, int> _data = new ();

        public void OnLoad() => ConvertListDataToDic();
        
        public void OnSave() => ConvertDicToListData();

#if UNITY_EDITOR
        public void LoadDataTest()
        {
            SetIntData(KeyData.Coin, coin);
            SetIntData(KeyData.Level, level);
        }
#endif

        // Set data for list, array
        public int GetItemState(ItemType itemType, Enum enumId)
        {
            int id = Convert.ToInt32(enumId);
            KeyData key = (KeyData) Enum.Parse(typeof(KeyData), itemType.ToString() + id);
            return GetIntData(key);
        }
        
        /// <summary>
        /// State: 0 = lock , 1 = unlock , 2 = equipped
        /// </summary>
        /// 
        public void SetItemState(ItemType itemType, Enum enumId, int state)
        {
            int id = Convert.ToInt32(enumId);
            KeyData key = (KeyData) Enum.Parse(typeof(KeyData), itemType.ToString() + id);
            SetIntData(key, state);
        }

        public int GetIntData(KeyData key, int defaultValue = 0)
        {
            if (_data.TryGetValue(key, out var data))
            {
                return data;
            }

            _data[key] = defaultValue;
            return defaultValue;
        }

        public void SetIntData(KeyData key, int value)
        {
            _data[key] = value;
        }

        public void OnEquipItem(ItemType itemType, Enum enumId)
        {
            int id = Convert.ToInt32(enumId);
            
            switch (itemType)
            {
                case ItemType.Weapon:
                    SetIntData(KeyData.PlayerWeapon, id);
                    break;
                case ItemType.Hair:
                    SetIntData(KeyData.PlayerHair, id);
                    break;
                case ItemType.Shield:
                    SetIntData(KeyData.PlayerShield, id);
                    break;
                case ItemType.Pant:
                    SetIntData(KeyData.PlayerPant, id);
                    break;
                case ItemType.SetSkin:
                    SetIntData(KeyData.PlayerSetSkin, id);
                    break;
            }
        }

        public void ConvertDicToListData()
        {
            listData.Clear();

            foreach (var data in _data)
            {
                listData.Add(new Data
                {
                    key = data.Key,
                    value = data.Value
                });
            }
        }

        private void ConvertListDataToDic()
        {
            foreach (var data in listData)
            {
                _data[data.key] = data.value;
            }
        }
    }
}