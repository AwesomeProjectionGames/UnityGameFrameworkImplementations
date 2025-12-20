using UnityEngine;
using UnityEngine.Events;

namespace GameFramework.Spectating
{
    /// <summary>
    /// A base implementation of the ICamera interface using MonoBehaviour.
    /// More friendly to inspector (that cannot serialize interfaces).
    /// </summary>
    public abstract class AbstractCamera : MonoBehaviour, ICamera
    {
        public abstract bool IsActive { get; set; }
        public abstract float FieldOfView { get; set; }
        public abstract float NearClipPlane { get; set; }
        public abstract float FarClipPlane { get; set; }
        
        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }
        public Quaternion Rotation
        {
            get => transform.rotation;
            set => transform.rotation = value;
        }
        
        [Tooltip("Invoked when the state of the camera changed.")]
        public UnityEvent<bool> OnActiveStateChanged { get; } = new UnityEvent<bool>();
    }
}