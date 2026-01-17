#if MIRROR
using Mirror;
using UnityEngine;

namespace UnityGameFrameworkImplementations.BaseImplementation
{
    /// <summary>
    /// A concrete GameInstance implementation for Mirror.
    /// Acts as a bridge to the NetworkManager for starting sessions.
    /// </summary>
    public class MirrorGameInstance : AbstractGameInstance
    {
        public void StartHost()
        {
            if (NetworkManager.singleton == null)
            {
                Debug.LogError("[MirrorGameInstance] NetworkManager is missing!");
                return;
            }
            NetworkManager.singleton.StartHost();
        }

        public void StartServer()
        {
            if (NetworkManager.singleton == null)
            {
                Debug.LogError("[MirrorGameInstance] NetworkManager is missing!");
                return;
            }
            NetworkManager.singleton.StartServer();
        }

        public void StartClient()
        {
            if (NetworkManager.singleton == null)
            {
                Debug.LogError("[MirrorGameInstance] NetworkManager is missing!");
                return;
            }
            NetworkManager.singleton.StartClient();
        }
    }
}
#endif
