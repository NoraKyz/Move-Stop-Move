using System.Collections.Generic;
using _Game.Scripts.Character.Player;
using _Game.Utils;
using _Pattern.Singleton;
using UnityEngine;

namespace _Game.Scripts.Manager.Level
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private List<Level> levels = new List<Level>();
        
        private Level _currentLevel;
        public float MaxDistanceMap { get; private set; }
        public void OnLoadLevel(int level)
        {
            if (_currentLevel != null)
            {
                Destroy(_currentLevel.gameObject);
            }

            _currentLevel = Instantiate(levels[level]);
            MaxDistanceMap = 50f; // TODO: Get from level config
        }
        
        [SerializeField] private Player player;
        
        private List<Character.Character> _bots = new List<Character.Character>();
        private void NewBot()
        {
            Character.Character bot = SimplePool.Spawn<Character.Character>(PoolType.Bot, RandomPoint(), Quaternion.identity);
            
            bot.OnInit();
            _bots.Add(bot);
            
            bot.SetScore(player.Score > 0 ? Random.Range(player.Score - 7, player.Score + 7) : 1);
        }

        public Vector3 RandomPoint()
        {
            return Utilities.GetRandomPosOnNavMesh(Vector3.zero, MaxDistanceMap);
        }
    }
}