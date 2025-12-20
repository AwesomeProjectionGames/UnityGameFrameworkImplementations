#nullable enable

using AwesomeProjectionCoreUtils.Extensions;
using UnityEngine;

namespace GameFramework.Spectating
{
    /// <summary>
    /// A simple ISpectate implementation for static objects in full screen.
    /// It's intended for the default thing to spectate, before any entity has spawned (like a lobby camera).
    /// </summary>
    public class FullScreenSpectatedStatic : GameObjectUnityEventsSpectated
    {
        [SerializeField] bool requestSpectateOnEnable = true;

        private void OnEnable()
        {
            if (requestSpectateOnEnable)
            {
                if (Spectating.SpectateController.FullScreen.IsAlive())
                {
                    Spectating.SpectateController.FullScreen!.Spectate(this);
                }
                else
                {
                    Debug.LogWarning("FullScreen SpectateController is not already available.");
                }
            }
        }
    }
}