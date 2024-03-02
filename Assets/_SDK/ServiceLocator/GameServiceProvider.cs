using System;
using System.Collections.Generic;
using UnityEngine;

namespace _SDK.ServiceLocator
{
    [CreateAssetMenu(fileName = "GameServiceProvider", menuName = "Game Service Provider")]
    public class GameServiceProvider : ScriptableObject
    {
        private Dictionary<Type, GameService> _masterServicesCollection = new Dictionary<Type, GameService>();
        
        public void AddGameService(GameService gameService)
        {
            if (!_masterServicesCollection.ContainsKey(gameService.GetType()))
            {
                _masterServicesCollection.Add(gameService.GetType(), gameService);
            }
            else
            {
                Debug.LogError("GameService of type" + gameService.GetType().ToString() + 
                               " is already contained within Master Services Collection");
            }
        }
        
        public T GetGameService<T>() where T : GameService
        {
            if (_masterServicesCollection.TryGetValue(typeof(T), out GameService service))
            {
                return (T)service;
            }
        
            return null;
        }
        
        public void ClearServices()
        {
            _masterServicesCollection.Clear();
        }
        
        public void Initialize()
        {
            foreach (var item in _masterServicesCollection)
            {
                item.Value.Initialize(this);
            }
        }
    }
}