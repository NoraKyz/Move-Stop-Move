using System.Collections.Generic;
using _Game.Scripts.GamePlay.Character;
using _Game.Scripts.GamePlay.Character.Bot;
using _Game.Scripts.Other.Utils;
using _SDK.ServiceLocator.Scripts;
using _SDK.UI.Base;
using UnityEngine;

namespace _Game.Scripts.Level
{
    public class LevelManager : GameService
    {
        #region Config

        [Header("References")]
        [SerializeField] private List<LevelDataSO> levels = new List<LevelDataSO>();
        
        [Header("Config")]
        [SerializeField] private int currentLevelId;
        [SerializeField] private GameObject currentMapPrefab;
        
        private int _totalBot;
        private int _totalCharacter;
        private float _maxDistanceMap;
        
        public int TotalCharacter => _totalCharacter;

        #endregion
        
        public void OnLoadLevel(int levelId)
        {
            if (currentMapPrefab != null)
            {
                Destroy(currentMapPrefab);
                this.GetService<CharacterManager>().DespawnAllCharacter();
            }
            
            currentLevelId = levelId;   
            currentMapPrefab = Instantiate(levels[levelId].mapPrefab);
            
            SetUpLevel(currentLevelId);
        }
        
        private void SetUpLevel(int id)
        {
            _maxDistanceMap = levels[id].maxDistanceMap;
            _totalCharacter = levels[id].totalCharacter;
            _totalBot = levels[id].totalCharacter - 1;
            
            for(int i = 0; i < Constants.MaxBotOnMap; i++)
            {
                if (_totalBot > 0)
                {
                    _totalBot--;
                    this.GetService<CharacterManager>().NewBot();
                }
            }
            
            this.GetService<CharacterManager>().NewPlayer();
        }
        
        public void BotDeath(Bot bot)
        {
            this.GetService<CharacterManager>().RemoveBot(bot);

            if (GameManager.IsState(GameState.Revive) || GameManager.IsState(GameState.Setting))
            {
                this.GetService<CharacterManager>().NewBot();
            }
            else
            {
                if (_totalBot > 0)
                {
                    _totalBot--;
                    this.GetService<CharacterManager>().NewBot();
                }
                else if (_totalBot == 0)
                {
                    Victory();
                }   
            }
        }

        private void Victory()
        {
            
        }
        
        public Vector3 RandomPoint() => Utilities.GetRandomPosOnNavMesh(Vector3.zero, _maxDistanceMap);
        
    }
}