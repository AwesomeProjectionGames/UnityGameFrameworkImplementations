using UnityEngine;
using GameFramework;
using AwesomeProjectionCoreUtils.Extensions;

namespace GameFramework.Spectating
{
    public static class ICameraExtensions
    {
        public static bool IsValid(this ICamera? camera)
        {
            if (camera == null) return false;
            if (camera is Object) return camera.IsAlive();
            return true;
        }

        public static void CopyFrom(this ICamera target, ICamera source, bool copyRect = false)
        {
            if (!target.IsValid() || !source.IsValid()) return;
            
            target.Position = source.Position;
            target.Rotation = source.Rotation;
            target.FieldOfView = source.FieldOfView;
            target.NearClipPlane = source.NearClipPlane;
            target.FarClipPlane = source.FarClipPlane;
            
            if (copyRect)
            {
                target.Rect = source.Rect;
            }
        }

        public static void LerpFrom(this ICamera target, ICamera from, ICamera to, float t)
        {
             if (!target.IsValid() || !from.IsValid() || !to.IsValid()) return;

             target.Position = Vector3.Lerp(from.Position, to.Position, t);
             target.Rotation = Quaternion.Slerp(from.Rotation, to.Rotation, t);
             target.FieldOfView = Mathf.Lerp(from.FieldOfView, to.FieldOfView, t);
             target.NearClipPlane = Mathf.Lerp(from.NearClipPlane, to.NearClipPlane, t);
             target.FarClipPlane = Mathf.Lerp(from.FarClipPlane, to.FarClipPlane, t);
        }
    }
}
