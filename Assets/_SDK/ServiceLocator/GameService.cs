using UnityEngine;

namespace _SDK.ServiceLocator
{
    public class GameService : MonoBehaviour
    {
        protected GameServiceProvider ServiceProvider;
        public virtual void Initialize(GameServiceProvider gameService)
        {
            ServiceProvider = gameService;
        }
    }
}