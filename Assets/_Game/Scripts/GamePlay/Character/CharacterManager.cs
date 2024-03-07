using System.Collections.Generic;
using _SDK.Pool.Scripts;
using _SDK.ServiceLocator.Scripts;
using UnityEngine;

namespace _Game.Scripts.GamePlay.Character
{
    public class CharacterManager : GameService
    {
        [Header("References")]
        [SerializeField] private Player.Player player;
        [SerializeField] private List<Bot.Bot> bots = new List<Bot.Bot>();
        
        private Map.Map _currentMap;
        
        public void SetMap(Map.Map map) => _currentMap = map;
        
        public void ResetPlayer() => player.OnInit();
        
        public void NewBot()
        {
            Bot.Bot bot = SimplePool.Spawn<Bot.Bot>(PoolType.Bot, _currentMap.GetRandomPos(), Quaternion.identity);
            
            bot.OnInit();
            //bot.SetScore(player.Score > 0 ? Random.Range(player.Score - 7, player.Score + 7) : 1);
            
            bots.Add(bot);
        }
        
        public void RemoveBot(Bot.Bot bot) => bots.Remove(bot);
        
        public void ClearAll()
        {
            for (int i = 0; i < bots.Count; i++)
            {
                if (bots[i] != null)
                {
                    bots[i].OnDespawn();
                }
            }
            
            player.OnDespawn();
        }
    }
}