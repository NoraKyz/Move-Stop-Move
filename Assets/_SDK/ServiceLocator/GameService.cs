using UnityEngine;

namespace _SDK.ServiceLocator
{
    public class GameService : MonoBehaviour
    {
        // Help to get other services without using Singleton
        protected GameServiceProvider serviceProvider;
        
        public virtual void Initialize(GameServiceProvider gameService)
        {
            serviceProvider = gameService;
        }
    }
}