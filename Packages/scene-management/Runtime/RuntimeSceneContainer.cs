using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace KekwDetlef.SceneManagement.Runtime
{
    public static class RuntimeSceneContainer
    {
        public static Dictionary<SceneMap.UIScene, AsyncOperationHandle<SceneInstance>> activeUISceneMap = new Dictionary<SceneMap.UIScene, AsyncOperationHandle<SceneInstance>>();
        public static Dictionary<SceneMap.WorldScene, AsyncOperationHandle<SceneInstance>> activeWorldSceneMap = new Dictionary<SceneMap.WorldScene, AsyncOperationHandle<SceneInstance>>();
        // public static Dictionary<AssetReference, AsyncOperationHandle<SceneInstance>> preloadedSceneMap = new Dictionary<AssetReference, AsyncOperationHandle<SceneInstance>>();
    }
}
