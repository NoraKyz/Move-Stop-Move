using UnityEngine;

namespace _SDK.ServiceLocator
{
    public class GameServiceManager : MonoBehaviour
    {
        [SerializeField] private GameServiceProvider GameServiceProvider;
        private static GameServiceManager singletonInstance;
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (singletonInstance == null)
            {
                singletonInstance = this;
                GameServiceProvider.ClearServices();
                GameService[] gameSystems = GetComponentsInChildren<GameService>();
                for (int i = 0; i < gameSystems.Length; i++)
                {
                    GameServiceProvider.AddGameService(gameSystems[i]);
                }
                GameServiceProvider.Initialize();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}