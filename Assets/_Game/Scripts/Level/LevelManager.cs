using _Game.Scripts.Data;
using _Game.Scripts.GamePlay.Character;
using _Game.Scripts.GamePlay.Map;
using _SDK.Singleton;
using UnityEngine;

namespace _Game.Scripts.Level
{
    public class LevelManager : Singleton<LevelManager>
    {
        #region Config
        
        [Header("Config")] 
        [SerializeField] private LevelDataSO levelData;
        [SerializeField] private MapDataSO mapData;
        
        private Level _currentLevel;
        private Map _currentMap;
        
        public Level CurrentLevel => _currentLevel;
        public Map CurrentMap => _currentMap;

        #endregion
        
        public void LoadLevel(int levelId)
        {
            if (_currentMap != null)
            {
                Destroy(_currentMap.gameObject);
                CharacterManager.Ins.ClearAllCharacter();
            }
            
            _currentLevel = levelData.GetLevel(levelId);
            _currentMap = Instantiate(mapData.GetMap(_currentLevel.MapId));
            
            CharacterManager.Ins.SetMap(_currentMap);
            LevelGameManager.Ins.SetUpLevel(_currentLevel);
        }
        
        public void LoadNextLevel()
        {
            PlayerData playerData = DataManager.Ins.PlayerData;
            playerData.Level++;
            LoadLevel(playerData.Level);
        }
        
        public void LoadCurrentLevel()
        {
            LoadLevel(DataManager.Ins.PlayerData.Level);
        }
    }
}