using UnityEngine;

namespace UnityGameFrameworkImplementations.Core
{
#if !DISABLE_BOOTSTRAPPER
    /// <summary>
    /// Guarantee the game instance exists before the first scene even finishes loading.
    /// Automatically instanciate so you don't need to creat a prefab yourslef.
    /// For teyting, add flag DISABLE_BOOTSTRAPPER if you do not want this happening automatically.
    /// Or you can set a dummy GameInstance.Instance before this execute.
    /// </summary>
    public static class GameBootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void InitializeGameFramework()
        {
            if (GameInstance.Instance != null) return;
            var hostGo = new GameObject("[GameInstance]");
            var instance = hostGo.AddComponent<GameInstance>();
            Object.DontDestroyOnLoad(hostGo);
        }
    }
#endif
}