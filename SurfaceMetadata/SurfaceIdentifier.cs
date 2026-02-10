using UnityEngine;
using UnityEngine.Serialization;

namespace GameFramework.SurfaceMetadata
{
    public class SurfaceIdentifier : MonoBehaviour, ISurfaceModifier
    {
        public SurfaceMeta Meta => meta;
        
        [FormerlySerializedAs("Meta")] [SerializeField] 
        private SurfaceMeta meta;
    }
}