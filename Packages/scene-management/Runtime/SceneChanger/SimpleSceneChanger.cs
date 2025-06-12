using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace KekwDetlef.SceneManagement.Runtime
{
    public class SimpleSceneChanger : MonoBehaviour
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

        public void ChangeScene()
        {
            AsyncOperationHandle<SceneInstance> handle;
            AssetReference sceneAsset;

            if (sceneType == SceneType.UI)
            {
                sceneAsset = SceneMap.uiSceneMap[uiSceneInfo.type];
                handle = Addressables.LoadSceneAsync(sceneAsset, loadMode, true, uiSceneInfo.priority);

            }
            else // -> sceneType = SceneTypeToLoad.UI
            {
                sceneAsset = SceneMap.worldSceneMap[worldSceneInfo.type];
                handle = Addressables.LoadSceneAsync(sceneAsset, loadMode, true, worldSceneInfo.priority);
            }
            
            RuntimeSceneContainer.activeSceneMap.Add(sceneAsset, handle);
        }
    }
}
