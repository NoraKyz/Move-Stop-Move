using _Game.Scripts.GamePlay.Character;
using _Game.Scripts.GamePlay.Character.Bot;
using _Game.Scripts.Other.Utils;
using _SDK.ServiceLocator.Scripts;

namespace _Game.Scripts.Level
{
    public class LevelGameManager : GameService
    {
        private int _totalBots;
    
        public void SetUpLevel(Level level)
        {
            _totalBots = level.TotalBots;
            
            for(int i = 0; i < Constants.MaxBotOnMap; i++)
            {
                if (_totalBots > 0)
                {
                    _totalBots--;
                    this.GetService<CharacterManager>().NewBot();
                }
            }
            
            this.GetService<CharacterManager>().ResetPlayer();
        }
        
        public void BotDeath(Bot bot)
        {
            this.GetService<CharacterManager>().RemoveBot(bot);

            
            if (_totalBots > 0)
            {
                _totalBots--;
                this.GetService<CharacterManager>().NewBot();
            }
            else if (_totalBots == 0)
            {
                Victory();
            }   
        }

        private void Victory()
        {
            
        }
    }
}
