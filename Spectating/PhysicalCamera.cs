#nullable enable

using System;
using UnityEngine;
using UnityEngine.Events;

namespace GameFramework.Spectating
{
    /// <summary>
    /// Unity component that represents a physical camera in the scene (implementing ICamera, like a wrapper).
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class PhysicalCamera : AbstractCamera
    {
        public override bool IsActive
        {
            get => _camera.enabled;
            set
            {
                if (_camera.enabled != value)
                {
                    _camera.enabled = value;
                    if (_audioListener != null)
                    {
                        _audioListener.enabled = value;
                    }
                    OnActiveStateChanged.Invoke(value);
                }
            }
        }

        public override float FieldOfView
        {
            get => _camera.fieldOfView;
            set => _camera.fieldOfView = value;
        }

        public override float NearClipPlane
        {
            get => _camera.nearClipPlane;
            set => _camera.nearClipPlane = value;
        }

        public override float FarClipPlane
        {
            get => _camera.farClipPlane;
            set => _camera.farClipPlane = value;
        }
        
        private Camera _camera = null!;
        private AudioListener? _audioListener;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _audioListener = GetComponent<AudioListener>();
        }
    }
}