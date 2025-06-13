using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace KekwDetlef.SceneManagement.Runtime
{
    public class SimpleSceneLoader : MonoBehaviour
    {
        [SerializeField] private SceneType sceneType;
        [SerializeField] private LoadSceneMode loadMode; 
        [SerializeField] private UISceneInfo uiSceneInfo;
        [SerializeField] private WorldSceneInfo worldSceneInfo;

#if UNITY_EDITOR
        public string NO_sceneType => nameof(sceneType);
        public string NO_loadMode => nameof(loadMode);
        public string NO_uiSceneInfo => nameof(uiSceneInfo);
        public string NO_worldSceneInfo => nameof(worldSceneInfo);
#endif

        public void LoadScene()
        {
            AsyncOperationHandle<SceneInstance> handle;
            AssetReference sceneAsset;

            if (loadMode == LoadSceneMode.Single)
            {
                RuntimeSceneContainer.activeUISceneMap.Clear();
                RuntimeSceneContainer.activeWorldSceneMap.Clear();
            }

            if (sceneType == SceneType.UI)
            {
                sceneAsset = SceneMap.uiSceneMap[uiSceneInfo.type];
                handle = Addressables.LoadSceneAsync(sceneAsset, loadMode, true, uiSceneInfo.priority);
                RuntimeSceneContainer.activeUISceneMap.Add(uiSceneInfo.type, handle);
            }
            else // -> sceneType = SceneTypeToLoad.UI
            {
                sceneAsset = SceneMap.worldSceneMap[worldSceneInfo.type];
                handle = Addressables.LoadSceneAsync(sceneAsset, loadMode, true, worldSceneInfo.priority);
                RuntimeSceneContainer.activeWorldSceneMap.Add(worldSceneInfo.type, handle);
            }
        }
    }
}
