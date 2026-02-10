using UnityEngine;

namespace GameFramework.SurfaceMetadata
{
    [CreateAssetMenu(fileName = "SurfaceMeta", menuName = "AwesomeProjection/Surface Metadata/SurfaceMeta", order = 1)]
    public class SurfaceMeta : ScriptableObject
    {
        public string SurfaceName;
        public AudioClip[] FootstepSounds;
        public AudioClip[] JumpSounds;
        public AudioClip[] LandingSounds;
        public AudioClip[] CrouchSounds;
        public AudioClip[] SlideSounds;
        public AudioClip[] ImpactSounds;
        public float FootstepVolume = 1f;
    }
}