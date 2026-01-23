using System;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Bus;
using GameFramework.Dependencies;
using UnityEngine;
using UnityGameFrameworkImplementations.BaseImplementation.Inventory;
using UnityGameFrameworkImplementations.Communications;

namespace UnityGameFrameworkImplementations.BaseImplementation
{
    [DefaultExecutionOrder(-9999)] // Ensures this initializes before ANY other script
    public abstract class AbstractGameInstance : MonoBehaviour, IGameInstance
    {
        public static IGameInstance Instance { get; private set; }

        private DeferredEventBus _eventBus = new DeferredEventBus();
        private ComponentsContainer _services = new ComponentsContainer();

        public IEventBus EventBus => _eventBus;
        public IComponentsContainer Services => _services;

        private void Awake()
        {
            InitializeSingleton();
        }

        private void InitializeSingleton()
        {
            if (Instance != null && Instance != this)
            {
                // If a GameInstance already exists, destroy this new duplicate.
                // This happens when you load a Menu scene again (or any scene with a GameInstance)
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            _services.RegisterComponents(GetServices());
        }

        protected virtual void Update()
        {
            _eventBus.Tick(Time.deltaTime);
        }

        /// <summary>
        /// At the start of the game, register all services to be used via dependency injection.
        /// All services returned by this method will be registered in the Services container.
        /// </summary>
        protected virtual IEnumerable<object> GetServices()
        {
            yield return new ResourcesItemRegistry();
        }
        
        private void OnDestroy()
        {
            // Clean up static reference if this specific instance is destroyed
            if (Instance == this) Instance = null;
        }
    }
}