using _Game.Scripts.Data;
using _Game.Scripts.GamePlay.Input;
using _Game.Scripts.Level;
using _SDK.ServiceLocator.Scripts;
using _SDK.Singleton;
using UnityEngine;

namespace _SDK.UI.Base
{
    public enum GameState
    {
        MainMenu = 0,
        GamePlay = 1,
        Lose = 2,
        Revive = 3,
        Setting = 4,
        Win = 5,
    }
    
    public class GameManager : Singleton<GameManager>
    {
        private PlayerData PlayerData => this.GetService<DataManager>().PlayerData;
        
        //[SerializeField] CSVData csv;
        
        private static GameState _gameState;
        
        public static void ChangeState(GameState state)
        {
            _gameState = state;
            
            Ins.OnChangedState(state);
        }
        
        public static bool IsState(GameState state) => _gameState == state;
        
        private void Awake()
        {
            // Tránh người chơi chạm đa điểm vào màn hình
            Input.multiTouchEnabled = false;
            
            // Đặt target frame rate về 60 fps
            Application.targetFrameRate = 60;
            
            // Tránh tắt màn hình khi đang chơi game
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            // Xử lý tai thỏ
            int maxScreenHeight = 1280;
            float ratio = 1.0f * Screen.currentResolution.width / Screen.currentResolution.height;
            if (Screen.currentResolution.height > maxScreenHeight)
            {
                Screen.SetResolution(Mathf.RoundToInt(ratio * maxScreenHeight), maxScreenHeight, true);
            }
            
            //csv.OnInit();
            this.GetService<DataManager>().LoadData();
        }
        
        private void Start()
        {
            ChangeState(GameState.MainMenu);
        }
        
        private void OnChangedState(GameState state)
        {
            switch (state)
            {
                case GameState.MainMenu:
                    OnMainMenuState();
                    break;
                case GameState.GamePlay:
                    OnGamePlayState();
                    break;
                case GameState.Revive:
                    OnReviveState();
                    break;
                case GameState.Lose:
                    OnLoseState();
                    break;
                case GameState.Setting:
                    break;
            }
        }
        
        private void OnMainMenuState()
        {
            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<UIMainMenu>();
            this.GetService<LevelManager>().OnLoadLevel(PlayerData.Level);
        }
        
        private void OnGamePlayState()
        {
            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<GamePlay.UIGamePlay>();
            this.GetService<InputManager>().GetInputEntity();
        }
        
        private void OnReviveState()
        {
            UIManager.Ins.OpenUI<Revive.UIRevive>();
        }
        
        private void OnLoseState()
        {
            UIManager.Ins.CloseUI<Revive.UIRevive>();
            UIManager.Ins.OpenUI<UILose>();
        }
    }
}