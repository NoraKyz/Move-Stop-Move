using _SDK.ServiceLocator.Scripts;
using UnityEditor;
using UnityEngine;

namespace _Game.Scripts.Data
{
    public class DataManager : GameService
    {
        private const string PlayerDataKey = "PlayerData";

        [SerializeField] private PlayerData playerData;
        
        public PlayerData PlayerData => playerData;

        private void OnApplicationPause(bool pause)
        {
            SaveData();
            //FirebaseManager.Ins.OnSetUserProperty();
        }
        
        private void OnApplicationQuit()
        {
            SaveData();
            //FirebaseManager.Ins.OnSetUserProperty();
        }

        public void LoadData()
        {
            string data = PlayerPrefs.GetString(PlayerDataKey, "");
            
            if (data != "")
            {
                playerData = JsonUtility.FromJson<PlayerData>(data);
            }
            else
            {
                playerData = new PlayerData();
            }
            
            playerData.OnLoad();
            
            //FirebaseManager.Ins.OnSetUserProperty();
        }

        public void SaveData()
        {
            playerData.OnSave();
            
            string json = JsonUtility.ToJson(playerData);
            PlayerPrefs.SetString(PlayerDataKey, json);
        }

#if UNITY_EDITOR
        public void ResetData()
        {
            playerData = new PlayerData();
            PlayerPrefs.DeleteAll();
        }
        
        public void LoadDataTest()
        {
            playerData.LoadDataTest();
            SaveData();
        }
        
        public void UpdateDataInspector() => playerData.ConvertDicToListData();
    }
    
    [CustomEditor(typeof(DataManager))]
    public class DataManagerEditor : Editor
    {
        private DataManager _dataManager;
        
        private void OnEnable()
        {
            _dataManager = (DataManager) target;
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (GUILayout.Button("Clear Data"))
            {
                _dataManager.ResetData();
                EditorUtility.SetDirty(_dataManager);
            }
            
            if (GUILayout.Button("Upload Data"))
            {
                _dataManager.UpdateDataInspector();
                EditorUtility.SetDirty(_dataManager);
            }
            
            if (GUILayout.Button("Load Data Test"))
            {
                _dataManager.LoadDataTest();
                EditorUtility.SetDirty(_dataManager);
            }
        }
    }
#endif
}