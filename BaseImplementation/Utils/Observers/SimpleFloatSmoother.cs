using System;
using UnityEngine;
using UnityGameFrameworkImplementations.Communications;

namespace UnityGameFrameworkImplementations.Core.Utils
{
    /// <summary>
    /// A Unity component that smoothly interpolates a float value over time.
    /// It observes changes from a source and exposes the smoothed value for effect systems.
    /// </summary>
    public class SimpleFloatSmoother : ValueObserverMonoBehaviour<float>, IObservable<float>
    {
        public string Name => smoothedChannelName;
        public float CurrentValue { get; private set; }
        public event Action<float> OnValueChanged;
        
        [SerializeField] private string smoothedChannelName = "SmoothedValue";
        [SerializeField] private float smoothingSpeed = 5f;
        
        private float _targetValue;
        
        protected override void OnObservedValueChanged(float newValue)
        { 
            _targetValue = newValue;
        }

        private void Update()
        {
            if (Mathf.Approximately(CurrentValue, _targetValue))
                return;

            CurrentValue = Mathf.Lerp(CurrentValue, _targetValue, Time.deltaTime * smoothingSpeed);
            OnValueChanged?.Invoke(CurrentValue);
        }

        public IDisposable Subscribe(IObserver<float> observer)
        {
            throw new NotImplementedException();
        }
    }
}