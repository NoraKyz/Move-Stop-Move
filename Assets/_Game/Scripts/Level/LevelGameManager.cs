using _Game.Scripts.GamePlay.Character;
using _Game.Scripts.GamePlay.Character.Bot;
using _Game.Scripts.Other.Utils;
using _SDK.ServiceLocator.Scripts;
using _SDK.UI;
using _SDK.UI.Base;

namespace _Game.Scripts.Level
{
    public class LevelGameManager : GameService
    {
        private int _totalBotsAlive;
        private int _totalBotsValid;
        
        public int TotalBotsAlive => _totalBotsAlive;
        
        public void SetUpLevel(Level level)
        {
            _totalBotsAlive = level.TotalBots;
            _totalBotsValid = level.TotalBots;
            
            for(int i = 0; i < Constants.MaxBotOnMap; i++)
            {
                if (_totalBotsValid > 0)
                {
                    _totalBotsValid--;
                    this.GetService<CharacterManager>().NewBot();
                }
            }
            
            this.GetService<CharacterManager>().ResetPlayer();
            this.GetService<CharacterManager>().SetTargetIndicatorAlpha(0f);
        }
        
        public void BotDeath(Bot bot)
        {
            this.GetService<CharacterManager>().RemoveBot(bot);
            
            if (_totalBotsValid > 0)
            {
                _totalBotsValid--;
                this.GetService<CharacterManager>().NewBot();
            }

            _totalBotsAlive--;
            
            if (_totalBotsAlive == 0)
            {
                Victory();
            }   
        }

        private void Victory()
        {
            GameManager.ChangeState(GameState.Finish);
            UIManager.Ins.OpenUI<UIVictory>();
        }
    }
}
