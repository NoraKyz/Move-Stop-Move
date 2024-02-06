using _Game.Scripts.Input;
using _Game.Scripts.Manager;
using _Game.Scripts.Manager.Level;
using _Pattern;
using _Pattern.Event.Scripts;
using _Pattern.Singleton;
using UnityEngine;

namespace _UI.Scripts.UI
{
    public enum GameState
    {
        MainMenu = 1,
        GamePlay = 2,
        Lose = 3,
        Revive = 4,
        Setting = 5,
        Win = 6,
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
            UIManager.Instance.OpenUI<MainMenu>();
            
            LevelManager.Instance.OnLoadLevel(0);
            
            //this.RemoveListenersByID(EventID.OnCharacterDie);
        }
        private void OnGamePlayState()
        {
            UIManager.Instance.CloseAll();
            UIManager.Instance.OpenUI<GamePlay.GamePlay>();
            
            InputManager.GetInputEntity();
        }
        private void OnReviveState()
        {
            UIManager.Instance.OpenUI<Revive.Revive>();
        }
        private void OnLoseState()
        {
            UIManager.Instance.CloseUI<Revive.Revive>();
            UIManager.Instance.OpenUI<Lose>();
        }
    }
}