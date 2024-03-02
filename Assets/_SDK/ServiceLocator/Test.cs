// using System;
// using System.Collections.Generic;
// using UnityEngine;
//
// [CreateAssetMenu(fileName = "GameServiceProvider", menuName = "Game Service Provider")]
// public class GameServiceProvider : ScriptableObject
// {
//     private Dictionary<Type, GameSystemService> masterServicesCollection = new Dictionary<Type, GameSystemService>();
//     public void AddGameService(GameSystemService gameService)
//     {
//         if (!masterServicesCollection.ContainsKey(gameService.GetType()))
//         {
//             masterServicesCollection.Add(gameService.GetType(), gameService);
//         }
//         else
//         {
//             Debug.LogError("GameService of type" + gameService.GetType().ToString() + 
//                 " is already contained within Master Services Collection");
//         }
//     }
//     public T GetGameService<T>() where T : GameSystemService
//     {
//         if (masterServicesCollection.TryGetValue(typeof(T), out GameSystemService service))
//         {
//             return (T)service;
//         }
//         
//         return null;
//     }
//     public void ClearServices()
//     {
//         masterServicesCollection.Clear();
//     }
//     public void Initialize()
//     {
//         foreach (var item in masterServicesCollection)
//         {
//             item.Value.Initialize(this);
//         }
//     }
// }
//
// public class GameSystemService : MonoBehaviour
// {
//     protected GameServiceProvider ServiceProvider;
//     public virtual void Initialize(GameServiceProvider gameService)
//     {
//         ServiceProvider = gameService;
//     }
// }
// public class GameManager : MonoBehaviour
// {
//     [SerializeField] private GameServiceProvider GameServiceProvider;
//     private static GameManager singletonInstance;
//     private void Awake()
//     {
//         DontDestroyOnLoad(gameObject);
//         if (singletonInstance == null)
//         {
//             singletonInstance = this;
//             GameServiceProvider.ClearServices();
//             GameSystemService[] gameSystems = GetComponentsInChildren<GameSystemService>();
//             for (int i = 0; i < gameSystems.Length; i++)
//             {
//                 GameServiceProvider.AddGameService(gameSystems[i]);
//             }
//             GameServiceProvider.Initialize();
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }
// }