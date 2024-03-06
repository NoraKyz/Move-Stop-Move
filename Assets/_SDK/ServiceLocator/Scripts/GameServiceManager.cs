using UnityEngine;

namespace _SDK.ServiceLocator.Scripts
{
    public class GameServiceManager : MonoBehaviour
    {
        [SerializeField] private GameServiceProvider gameServiceProvider;
        
        private static GameServiceManager _instance;
        
        public static GameServiceManager Instance => _instance;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            
            if (_instance == null)
            {
                _instance = this;
                
                SetupServices();
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void SetupServices()
        {
            gameServiceProvider.ClearServices();
            GameService[] gameSystems = GetComponentsInChildren<GameService>();
            for (int i = 0; i < gameSystems.Length; i++)
            {
                gameServiceProvider.AddGameService(gameSystems[i]);
            }
            gameServiceProvider.Initialize();
        }
        
        public T GetService<T>() where T : GameService
        {
            return gameServiceProvider.GetGameService<T>();
        }
        
        
        public void ClearServices()
        {
            gameServiceProvider.ClearServices();
        }
    }
    
    // Extension method to get service from MonoBehaviour
    public static class ServiceDispatcher
    {
        public static T GetService<T>(this MonoBehaviour user) where T : GameService
        {
            return GameServiceManager.Instance.GetService<T>();
        }
        
        public static void ClearServices(this MonoBehaviour user)
        {
            GameServiceManager.Instance.ClearServices();
        }
    }
}