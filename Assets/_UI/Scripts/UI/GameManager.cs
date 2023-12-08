using System;
using _Game.Scripts.Manager.Level;
using _Pattern.Singleton;
using UnityEngine;

namespace _UI.Scripts.UI
{
    public enum GameState
    {
        MainMenu = 1,
        GamePlay = 2,
        Finish = 3,
        Revive = 4,
        Setting = 5,
    }
    public enum GameResult
    {
        Win = 1,
        Lose = 2,
    }
    public class GameManager : Singleton<GameManager>
    {
        public static event Action OnMenuState;
        public static event Action OnGamePlayState;
        public static event Action OnSettingState;
        
        //[SerializeField] UserData userData;
        //[SerializeField] CSVData csv;
        
        [SerializeField] private Transform mainCamera;
        private static GameState _gameState;
        public Transform MainCamera => mainCamera;
        public static void ChangeState(GameState state)
        {
            _gameState = state;
            
            
        }
        public static bool IsState(GameState state) => _gameState == state;
        private void Awake()
        {
            // Tranh viec nguoi choi cham da diem vao man hinh
            Input.multiTouchEnabled = false;
            // Target frame rate ve 60 fps
            Application.targetFrameRate = 60;
            // Tranh viec tat man hinh
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            // Xu tai tho
            int maxScreenHeight = 1280;
            float ratio = 1.0f * Screen.currentResolution.width / Screen.currentResolution.height;
            if (Screen.currentResolution.height > maxScreenHeight)
            {
                Screen.SetResolution(Mathf.RoundToInt(ratio * maxScreenHeight), maxScreenHeight, true);
            }
            
            //csv.OnInit();
            //userData?.OnInitData();
            _gameState = GameState.MainMenu;
        }

        private void Start()
        {
            _gameState = GameState.MainMenu;
            UIManager.Instance.OpenUI<MainMenu>();
            LevelManager.Instance.OnLoadLevel(0);
        }
    }
}