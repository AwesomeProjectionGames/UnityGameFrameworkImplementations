using System;
using System.Collections.Generic;
using GameFramework.Reactive;
using UnityEngine;

namespace UnityGameFrameworkImplementations.Communications
{
    /// <summary>
    /// Unity-specific abstract base class used to expose a simple value of type <typeparamref name="T"/>
    /// through a MonoBehaviour, so visual effect systems (VFX Graph, shaders, UI, etc.)
    /// can bind and react to it.
    /// </summary>
    /// <typeparam name="T">The type of the value to expose.</typeparam>
    public abstract class ValueObservableMonoBehaviour<T> : MonoBehaviour, IValueObservable<T>
    {
        [SerializeField]
        [Tooltip("Identifier/channel used for auto-binding in effect systems.")]
        protected string _name = string.Empty;
        
        public string Name => _name;
        
        /// <summary>
        /// The current value being exposed.
        /// The current value can be set silently. To notify listeners, use SetValue instead.
        /// </summary>
        public T CurrentValue { get; protected set; } = default!;
        
        protected readonly List<IObserver<T>> Observers = new();

        /// <summary>
        /// Sets the exposed value.
        /// If the value changes, notifies listeners.
        /// </summary>
        /// <param name="value">New value to expose.</param>
        protected virtual void SetValue(T value)
        {
            if (Equals(CurrentValue, value))
                return;

            CurrentValue = value;
            NotifyObservers(value);
        }
        
        private void NotifyObservers(T value)
        {
            foreach (var observer in Observers)
            {
                observer.OnNext(value);
            }
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (!Observers.Contains(observer))
            {
                Observers.Add(observer);
                // Push the current value to the new subscriber
                observer.OnNext(CurrentValue);
            }
            return new Unsubscriber(Observers, observer);
        }
        
        private class Unsubscriber : IDisposable
        {
            private readonly List<IObserver<T>> _observers;
            private readonly IObserver<T> _observer;

            public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                {
                    _observers.Remove(_observer);
                }
            }
        }
    }
}