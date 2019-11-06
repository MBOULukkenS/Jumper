using UnityEngine;
using UnityEngine.Rendering;

namespace Utils
{
    public static class Preload
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void LoadAfterScene()
        {
        }
    }
}