using System.Collections.Generic;
using _Game.Scripts.Character.Bot;
using _Game.Scripts.Character.Player;
using _Game.Utils;
using _Pattern.Singleton;
using _UI.Scripts.UI;
using UnityEngine;

namespace _Game.Scripts.Manager.Level
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private List<Level> levels = new List<Level>();
        
        private Level _currentLevel;
        private int _totalBot;
        private float _maxDistanceMap;
        
        public void OnLoadLevel(int level)
        {
            if (_currentLevel != null)
            {
                Destroy(_currentLevel.gameObject);
            }

            _currentLevel = Instantiate(levels[level]);
            
            SetUpLevel();
        }
        private void SetUpLevel()
        {
            _maxDistanceMap = _currentLevel.MaxDistanceMap;
            _totalBot = _currentLevel.TotalBot;
            
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
        
        [SerializeField] private Player player;
        
        [SerializeField] private List<Character.Character> _bots = new List<Character.Character>();
        private void NewBot()
        {
            Character.Character bot = SimplePool.Spawn<Character.Character>(PoolType.Bot, RandomPoint(), Quaternion.identity);
            
            bot.OnInit();
            _bots.Add(bot);
            
            bot.SetScore(player.Score > 0 ? Random.Range(player.Score - 7, player.Score + 7) : 1);
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
        private void Victory()
        {
            
        }
        public Vector3 RandomPoint()
        {
            return Utilities.GetRandomPosOnNavMesh(Vector3.zero, _maxDistanceMap);
        }
    }
}