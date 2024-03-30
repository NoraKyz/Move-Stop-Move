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
        MainMenu, GamePlay, Finish, Revive, Setting
    }
    
    public class GameManager : Singleton<GameManager>
    {
        private PlayerData PlayerData => this.GetService<DataManager>().PlayerData;
        
        //[SerializeField] CSVData csv;
        
        private static GameState _gameState;
        
        public static void ChangeState(GameState state)
        {
            _gameState = state;
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
            UIManager.Ins.OpenUI<UIMainMenu>();
            PlayerData playerData = this.GetService<DataManager>().PlayerData;
            this.GetService<LevelManager>().LoadLevel(playerData.Level);
        }
    }
}