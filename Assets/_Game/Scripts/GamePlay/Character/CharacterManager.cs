using System.Collections.Generic;
using _SDK.Pool.Scripts;
using _SDK.Singleton;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts.GamePlay.Character
{
    public class CharacterManager : Singleton<CharacterManager>
    {
        #region Config

        [Header("References")]
        [SerializeField] private Player.Player player;
        [SerializeField] private List<Bot.Bot> listBots = new ();

        public Player.Player Player => player;
        public List<Bot.Bot> ListBots => listBots;
        
        private Map.Map _currentMap;

        #endregion
        
        public void SetMap(Map.Map map) => _currentMap = map;

        public void ResetPlayer(bool isRevive = false)
        {
            player.OnInit();
            
            if (isRevive)
            {
                player.TF.position = _currentMap.GetRandomSpawnPos();
            }
        }
        
        public void NewBot()
        {
            Bot.Bot bot = SimplePool.Spawn<Bot.Bot>(PoolType.Bot, _currentMap.GetRandomSpawnPos(), Quaternion.identity);
            
            bot.OnInit();
            bot.SetScore(player.Score > 0 ? Random.Range(player.Score - 4, player.Score + 4) : 1);
            
            listBots.Add(bot);
        }
        
        public void RemoveBot(Bot.Bot bot) => listBots.Remove(bot);
        
        public void ClearAllCharacter()
        {
            for (int i = 0; i < listBots.Count; i++)
            {
                if (listBots[i] != null)
                {
                    listBots[i].OnDespawn();
                }
            }
            
            player.OnDespawn();
        }
        
        public void SetTargetIndicatorAlpha(float alpha)
        {
            List<PoolUnit> list = SimplePool.GetAllUnitIsActive(PoolType.Indicator);

            for (int i = 0; i < list.Count; i++)
            {
                (list[i] as TargetIndicator)?.SetAlpha(alpha);
            }
        }
    }
}