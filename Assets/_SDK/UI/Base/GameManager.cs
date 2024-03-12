using _Game.Scripts.Data;
using _Game.Scripts.GamePlay.Input;
using _Game.Scripts.Level;
using _SDK.ServiceLocator.Scripts;
using _SDK.Singleton;
using _SDK.Utils;
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
        //[SerializeField] UserData userData;
        //[SerializeField] CSVData csv;
        
        private static GameState _gameState;
        
        public static void ChangeState(GameState state)
        {
            _gameState = state;
            
            Instance.OnChangedState(state);
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
                default:
                    Common.LogWarning("Not handle state: " + state, this);
                    break;
            }
        }
        
        private void OnMainMenuState()
        {
            UIManager.Instance.CloseAll();
            UIManager.Instance.OpenUI<UIMainMenu>();
            
            this.GetService<LevelManager>().OnLoadLevel(UserData.Ins.Level);
        }
        
        private void OnGamePlayState()
        {
            UIManager.Instance.CloseAll();
            UIManager.Instance.OpenUI<GamePlay.UIGamePlay>();
            
            this.GetService<InputManager>().GetInputEntity();
        }
        
        private void OnReviveState()
        {
            UIManager.Instance.OpenUI<Revive.UIRevive>();
        }
        
        private void OnLoseState()
        {
            UIManager.Instance.CloseUI<Revive.UIRevive>();
            UIManager.Instance.OpenUI<UILose>();
        }
    }
}