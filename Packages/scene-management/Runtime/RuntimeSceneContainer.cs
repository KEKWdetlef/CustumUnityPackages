using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace KekwDetlef.SceneManagement.Runtime
{
    public static class RuntimeSceneContainer
    {
        public static Dictionary<AssetReference, AsyncOperationHandle<SceneInstance>> activeSceneMap = new Dictionary<AssetReference, AsyncOperationHandle<SceneInstance>>();
        public static Dictionary<AssetReference, AsyncOperationHandle<SceneInstance>> preloadedSceneMap = new Dictionary<AssetReference, AsyncOperationHandle<SceneInstance>>();
    }
}
