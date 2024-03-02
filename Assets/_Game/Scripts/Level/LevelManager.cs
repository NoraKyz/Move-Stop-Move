using System.Collections.Generic;
using _Game.Scripts.GamePlay.Character.Bot;
using _Game.Scripts.GamePlay.Character.Player;
using _Game.Scripts.Other.Utils;
using _Game.Scripts.UI.Base;
using _SDK.Pool.Scripts;
using _SDK.Singleton;
using UnityEngine;

namespace _Game.Scripts.Level
{
    public class LevelManager : Singleton<LevelManager>
    {
        #region Config

        [Header("References")]
        [SerializeField] private List<LevelDataSO> levels = new List<LevelDataSO>();
        
        [Header("Config")]
        [SerializeField] private int currentLevelId;
        [SerializeField] private GameObject currentLevelPrefab;
        
        private int _totalBot;
        private int _totalCharacter;
        private float _maxDistanceMap;
        public int TotalCharacter => _totalCharacter;

        #endregion
        
        public void OnLoadLevel(int levelId)
        {
            if (currentLevelPrefab != null)
            {
                Destroy(currentLevelPrefab);
                CollectAllCharacter();
            }
            
            currentLevelId = levelId;   
            currentLevelPrefab = Instantiate(levels[levelId].mapPrefab);
            
            SetUpLevel(currentLevelId);
        }
        
        private void SetUpLevel(int id)
        {
            _maxDistanceMap = levels[id].maxDistanceMap;
            _totalCharacter = levels[id].totalCharacter;
            _totalBot = _totalCharacter - 1;
            
            for(int i = 0; i < Constants.MaxBotOnMap; i++)
            {
                if (_totalBot > 0)
                {
                    _totalBot--;
                    NewBot();
                }
            }
            
            player.OnInit();
        }

        #region Character

        [SerializeField] private Player player;
        private List<GamePlay.Character.Base.Character> _bots = new List<GamePlay.Character.Base.Character>();
        
        private void NewBot()
        {
            GamePlay.Character.Base.Character bot = SimplePool.Spawn<GamePlay.Character.Base.Character>(PoolType.Bot, RandomPoint(), Quaternion.identity);
            
            bot.OnInit();
            //bot.SetScore(player.Score > 0 ? Random.Range(player.Score - 7, player.Score + 7) : 1);
            
            _bots.Add(bot);
        }
        
        public void BotDeath(Bot character)
        {
            _bots.Remove(character);

            if (GameManager.IsState(GameState.Revive) || GameManager.IsState(GameState.Setting))
            {
                NewBot();
            }
            else
            {
                if (_totalBot > 0)
                {
                    _totalBot--;
                    NewBot();
                }
                
                if (_bots.Count == 0)
                {
                    Victory();
                }   
            }
        }
        
        private void CollectAllCharacter()
        {
            for (int i = 0; i < _bots.Count; i++)
            {
                Bot bot = _bots[i] as Bot;
                if (bot != null)
                {
                    bot.OnDespawn();
                }
            }
            
            player.OnDespawn();
        }
        
        #endregion
        
        private void Victory()
        {
            
        }
        
        public Vector3 RandomPoint() => Utilities.GetRandomPosOnNavMesh(Vector3.zero, _maxDistanceMap);
        
    }
}