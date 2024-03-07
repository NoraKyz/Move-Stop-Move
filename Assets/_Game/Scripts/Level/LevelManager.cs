using _Game.Scripts.GamePlay.Character;
using _Game.Scripts.Map;
using _SDK.ServiceLocator.Scripts;
using UnityEngine;

namespace _Game.Scripts.Level
{
    public class LevelManager : GameService
    {
        #region Config
        
        [Header("Config")] 
        [SerializeField] private LevelDataSO levelData;
        [SerializeField] private MapDataSO mapData;
        
        private Level _currentLevel;
        private Map.Map _currentMap;
        
        public Level CurrentLevel => _currentLevel;
        public Map.Map CurrentMap => _currentMap;

        #endregion
        
        public void OnLoadLevel(int levelId)
        {
            if (_currentMap != null)
            {
                Destroy(_currentMap.gameObject);
                this.GetService<CharacterManager>().ClearAll();
            }
            
            _currentLevel = levelData.GetLevel(levelId);
            _currentMap = Instantiate(mapData.GetMap(_currentLevel.MapId));
            
            this.GetService<CharacterManager>().SetMap(_currentMap);
            this.GetService<LevelGameManager>().SetUpLevel(_currentLevel);
        }
    }
}