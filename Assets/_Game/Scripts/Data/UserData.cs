using System;
using _Game.Scripts.Other.Utils;
using _SDK.Observer.Scripts;
using _SDK.UI.Shop.SkinShop;
using UnityEditor;
using UnityEngine;

namespace _Game.Scripts.Data
{
    [CreateAssetMenu(fileName = "UserData", menuName = "Data/UserData", order = 1)]
    public class UserData : ScriptableObject
    {
        #region Singleton

        private static UserData _ins;
        public static UserData Ins
        {
            get
            {
                if (_ins == null)
                {
                    UserData[] datas = Resources.LoadAll<UserData>("");

                    if (datas.Length == 1)
                    {
                        _ins = datas[0];
                    }
                    else
                    if (datas.Length == 0)
                    {
                        Debug.LogError("Can find ScriptableObject UserData");
                    }
                    else
                    {
                        Debug.LogError("have multiple ScriptableObject UserData");
                    }
                }

                return _ins;
            }
        }

        #endregion

        #region Data Key

        public const string KeyLevel = "Level";
        public const string KeyCoin = "Coin";
        public const string KeySoundIsOn = "SoundIsOn";
        public const string KeyVibrateIsOn = "VibrateIsOn";
        public const string KeyRemoveAds = "RemoveAds";
        public const string KeyTutorialIsShow = "TutorialIsShow";

        public const string KeyPlayerWeapon = "PlayerWeapon";
        public const string KeyPlayerHair = "PlayerHair";
        public const string KeyPlayerPant = "PlayerPant";
        public const string KeyPlayerShield = "PlayerShield";
        public const string KeyPlayerSet = "PlayerSet";

        public const string KeyWeaponData = "WeaponData";
        public const string KeyHairData = "HairData";
        public const string KeyPantData = "PantData";
        public const string KeyShieldData = "ShieldData";
        public const string KeySetData = "SetData";

        #endregion
        
        [SerializeField] private int level;
        [SerializeField] private int coin;

        [SerializeField] private bool soundIsOn = true;
        [SerializeField] private bool vibrateIsOn = true;
        [SerializeField] private bool removeAds;
        [SerializeField] private bool tutorialIsShow;

        [SerializeField] private WeaponType playerWeapon;
        [SerializeField] private HairType playerHair;
        [SerializeField] private PantType playerPant;
        [SerializeField] private ShieldType playerShield;
        [SerializeField] private SetType playerSet;

        private Action<object> _onEquipSkinItem;

        #region Getter

        public int Level => level;
        public int Coin => coin;
        
        public bool SoundIsOn => soundIsOn;
        public bool VibrateIsOn => vibrateIsOn;
        public bool RemoveAds => removeAds;
        public bool TutorialIsShow => tutorialIsShow;
        
        public WeaponType PlayerWeapon => playerWeapon;
        public HairType PlayerHair => playerHair;
        public PantType PlayerPant => playerPant;
        public ShieldType PlayerShield => playerShield;
        public SetType PlayerSet => playerSet;

        #endregion

        private void Awake()
        {
            _onEquipSkinItem = (param) => OnEquipSkinItem((SkinShopItem) param);
            EventManager.Instance.RegisterListener(EventID.OnEquipSkinItem, _onEquipSkinItem);
        }
        
        private void OnEquipSkinItem(SkinShopItem item)
        {
            switch (item.ShopType)
            {
                case ShopType.Hair:
                    playerHair = (HairType) item.ItemType;
                    break;
                case ShopType.Pant:
                    playerPant = (PantType) item.ItemType;
                    break;
                case ShopType.Shield:
                    playerShield = (ShieldType) item.ItemType;
                    break;
                case ShopType.Set:
                    playerSet = (SetType) item.ItemType;
                    break;
            }
        }

        //Example
        // UserData.Ins.SetInt(UserData.Key_Level, ref UserData.Ins.level, 1);

        //data for list
        /// <summary>
        ///  0 = lock , 1 = unlock , 2 = selected
        /// </summary>
        /// <param name="key"></param>
        /// <param name="id"></param>
        /// <param name="state"></param>
        public void SetDataState(string key, int id, int state)
        {
            PlayerPrefs.SetInt(key + id, state);
        }
        
        public int GetDataState(string key, int id, int state)
        {
            return PlayerPrefs.GetInt(key + id, state);
        }

        public void SetDataState(string key, int state)
        {
            PlayerPrefs.SetInt(key, state);
        }

        public int GetDataState(string key, int state)
        {
            return PlayerPrefs.GetInt(key, state);
        }

        /// <summary>
        /// Key_Name
        /// if(bool) true == 1
        /// </summary>
        /// <param name="key"></param>
        /// <param name="variable"></param>
        /// <param name="value"></param>
        public void SetIntData(string key, ref int variable, int value)
        {
            variable = value;
            PlayerPrefs.SetInt(key, value);
        } 
    
        public void SetBoolData(string key, ref bool variable, bool value)
        {
            variable = value;
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        public void SetFloatData(string key, ref float variable, float value)
        {
            variable = value;
            PlayerPrefs.GetFloat(key, value);
        }

        public void SetStringData(string key, ref string variable, string value)
        {
            variable = value;
            PlayerPrefs.SetString(key, value);
        }

        public void SetEnumData<T>(string key, ref T variable, T value)
        {
            variable = value;
            PlayerPrefs.SetInt(key, Convert.ToInt32(value));
        }

        public void SetEnumData<T>(string key,T value)
        {
            PlayerPrefs.SetInt(key, Convert.ToInt32(value));
        }

        public T GetEnumData<T>(string key, T defaultValue) where T : Enum
        {
            return (T)Enum.ToObject(typeof(T), PlayerPrefs.GetInt(key, Convert.ToInt32(defaultValue)));
        }

#if UNITY_EDITOR
        [Space(10)]
        [Header("---- Editor ----")]
        public bool isTest;
#endif

        public void OnInitData()
        {

#if UNITY_EDITOR
            if (isTest) return;
#endif

            level = PlayerPrefs.GetInt(KeyLevel, 0);
            coin = PlayerPrefs.GetInt(KeyCoin, 0);

            removeAds = PlayerPrefs.GetInt(KeyRemoveAds, 0) == 1;
            tutorialIsShow =  PlayerPrefs.GetInt(KeyTutorialIsShow, 0) == 1;
            soundIsOn =  PlayerPrefs.GetInt(KeySoundIsOn, 0) == 1;
            vibrateIsOn =  PlayerPrefs.GetInt(KeyVibrateIsOn, 0) == 1;

            playerWeapon = GetEnumData(KeyPlayerWeapon, WeaponType.WHammer);
            playerHair = GetEnumData(KeyPlayerHair, HairType.None);
            playerPant = GetEnumData(KeyPlayerPant, PantType.None);
            playerShield = GetEnumData(KeyPlayerShield, ShieldType.None);
            playerSet = GetEnumData(KeyPlayerSet, SetType.SAngel);
        }

        public void OnResetData()
        {
            PlayerPrefs.DeleteAll();
            OnInitData();
        }
    }
    

#if UNITY_EDITOR

    [CustomEditor(typeof(UserData))]
    public class UserDataEditor : Editor
    {
        private UserData _userData;

        private void OnEnable()
        {
            _userData = (UserData) target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Load Data"))
            {
                _userData.OnInitData();
                EditorUtility.SetDirty(_userData);
            }
            
            if(GUILayout.Button("Reset Data"))
            {
                _userData.OnResetData();
                EditorUtility.SetDirty(_userData);
            }
        }
    }
#endif
    
}