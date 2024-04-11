using _Game.Scripts.GamePlay.Character;
using _Game.Scripts.GamePlay.Character.Bot;
using _Game.Scripts.Other.Utils;
using _SDK.Singleton;
using _SDK.UI;
using _SDK.UI.Base;

namespace _Game.Scripts.Level
{
    public class LevelGameManager : Singleton<LevelGameManager>
    {
        private int _totalBotsAlive;
        private int _totalBotsValid;

        private CharacterManager CharacterManager => GamePlay.Character.CharacterManager.Ins;
        public int TotalBotsAlive => _totalBotsAlive;
        
        public void SetUpLevel(Level level)
        {
            _totalBotsAlive = level.TotalBots;
            _totalBotsValid = level.TotalBots;
            
            for(int i = 0; i < Constants.MAX_BOT_ON_MAP; i++)
            {
                if (_totalBotsValid > 0)
                {
                    _totalBotsValid--;
                    CharacterManager.NewBot();
                }
            }
            
            CharacterManager.ResetPlayer();
            CharacterManager.SetTargetIndicatorAlpha(0f);
        }
        
        public void BotDeath(Bot bot)
        {
            CharacterManager.RemoveBot(bot);
            
            if (_totalBotsValid > 0)
            {
                _totalBotsValid--;
                CharacterManager.NewBot();
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
