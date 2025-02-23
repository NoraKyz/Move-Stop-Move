﻿using UnityEngine;

namespace _SDK.Singleton
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] protected bool dontDestroyOnLoad = false;
        
        protected static T instance;

        private void Awake()
        {
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        public static T Ins
        {
            get
            {
                if (instance == null)
                {
                    // Find singleton
                    instance = FindObjectOfType<T>();

                    // Create new instance if one doesn't already exist.
                    if (instance == null)
                    {
                        // Need to create a new GameObject to attach the singleton to.
                        var singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T) + " (Singleton)";
                    }
                }
                
                return instance;
            }
        }
    }
}
