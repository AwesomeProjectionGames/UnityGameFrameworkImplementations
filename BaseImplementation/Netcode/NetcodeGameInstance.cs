#if NETCODE
using Unity.Netcode;
using UnityEngine;

namespace UnityGameFrameworkImplementations.BaseImplementation
{
    /// <summary>
    /// A concrete GameInstance implementation for Unity Netcode for GameObjects (NGO).
    /// Acts as a bridge to the NetworkManager for starting sessions.
    /// </summary>
    public class NetcodeGameInstance : AbstractGameInstance
    {
        public bool StartHost()
        {
            if (NetworkManager.Singleton == null)
            {
                Debug.LogError("[NetcodeGameInstance] NetworkManager is missing!");
                return false;
            }
            return NetworkManager.Singleton.StartHost();
        }

        public bool StartServer()
        {
            if (NetworkManager.Singleton == null)
            {
                Debug.LogError("[NetcodeGameInstance] NetworkManager is missing!");
                return false;
            }
            return NetworkManager.Singleton.StartServer();
        }

        public bool StartClient()
        {
            if (NetworkManager.Singleton == null)
            {
                Debug.LogError("[NetcodeGameInstance] NetworkManager is missing!");
                return false;
            }
            return NetworkManager.Singleton.StartClient();
        }
    }
}
#endif
