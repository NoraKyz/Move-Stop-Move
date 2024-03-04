using System;
using _Game.Scripts.Other.Utils;
using UnityEditor;
using UnityEngine;

namespace _Game.Scripts.Data
{
    public static class DataKey
    {
        public const string Level = "Level";
        public const string Coin = "Coin";
        public const string SoundIsOn = "SoundIsOn";
        public const string VibrateIsOn = "VibrateIsOn";
        public const string RemoveAds = "RemoveAds";
        public const string TutorialIsShow = "TutorialIsShow";

        public const string PlayerWeapon = "PlayerWeapon";
        public const string PlayerHair = "PlayerHair";
        public const string PlayerPant = "PlayerPant";
        public const string PlayerShield = "PlayerShield";
        public const string PlayerSkin = "PlayerSkin";

        public const string WeaponData = "WeaponData";
        public const string HairData = "HairData";
        public const string PantData = "PantData";
        public const string ShieldData = "ShieldData";
        public const string SkinData = "SkinData";
    }
    
    [CreateAssetMenu(fileName = "UserData", menuName = "Data/UserData", order = 1)]
    public class UserData : ScriptableObject
    {
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
        
        public int level = 0;
        public int coin = 0;

        public bool soundIsOn = true;
        public bool vibrateIsOn = true;
        public bool removeAds = false;
        public bool tutorialIsShow = false;

        public WeaponType playerWeapon;
        public HairType playerHair;
        public PantType playerPant;
        public ShieldType playerShield;
        //public SkinType playerSkin;

        //Example
        // UserData.Ins.SetInt(UserData.Key_Level, ref UserData.Ins.level, 1);

        //data for list
        /// <summary>
        ///  0 = lock , 1 = unlock , 2 = selected
        /// </summary>
        /// <param name="key"></param>
        /// <param name="ID"></param>
        /// <param name="state"></param>
        public void SetDataState(string key, int ID, int state)
        {
            PlayerPrefs.SetInt(key + ID, state);
        }
        /// <summary>
        ///  0 = lock , 1 = unlock , 2 = selected
        /// </summary>
        /// <param name="key"></param>
        /// <param name="id"></param>
        /// <param name="state"></param>
        public int GetDataState(string key, int id, int state = 0)
        {
            return PlayerPrefs.GetInt(key + id, state);
        }

        public void SetDataState(string key, int state)
        {
            PlayerPrefs.SetInt(key, state);
        }

        public int GetDataState(string key, int state = 0)
        {
            return PlayerPrefs.GetInt(key, state);
        }

        /// <summary>
        /// Key_Name
        /// if(bool) true == 1
        /// </summary>
        /// <param name="key"></param>
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

            level = PlayerPrefs.GetInt(DataKey.Level, 0);
            coin = PlayerPrefs.GetInt(DataKey.Coin, 0);

            removeAds = PlayerPrefs.GetInt(DataKey.RemoveAds, 0) == 1;
            tutorialIsShow =  PlayerPrefs.GetInt(DataKey.TutorialIsShow, 0) == 1;
            soundIsOn =  PlayerPrefs.GetInt(DataKey.SoundIsOn, 0) == 1;
            vibrateIsOn =  PlayerPrefs.GetInt(DataKey.VibrateIsOn, 0) == 1;

            playerWeapon = GetEnumData(DataKey.PlayerWeapon, WeaponType.Hammer);
            playerHair = GetEnumData(DataKey.PlayerHair, HairType.None);
            playerPant = GetEnumData(DataKey.PlayerPant, PantType.None);
            playerShield = GetEnumData(DataKey.PlayerShield, ShieldType.None);
            //playerSkin = GetEnumData(Key_Player_Skin, SkinType.SKIN_Normal);
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