using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace KekwDetlef.SceneManagement.Runtime
{
    public class MultipleSceneChanger : MonoBehaviour
    {
        [SerializeField] private UISceneInfo[] uiSceneInfo;
        [SerializeField] private WorldSceneInfo[] worldSceneInfo;

        public void ChangeScene()
        {
            ChangeUIScene();
            ChangeWorldScene();
        }

        private void ChangeWorldScene()
        {

            List<SceneMap.WorldScene> newScenesSet = new List<SceneMap.WorldScene>();
            foreach (var info in worldSceneInfo)
            {
                newScenesSet.Add(info.type);

                if (!RuntimeSceneContainer.activeWorldSceneMap.ContainsKey(info.type))
                {
                    AssetReference sceneAsset = SceneMap.worldSceneMap[info.type];
                    AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneAsset, LoadSceneMode.Additive, true, info.priority);
                    RuntimeSceneContainer.activeWorldSceneMap.Add(info.type, handle);
                }
            }

            foreach (var activeScene in RuntimeSceneContainer.activeWorldSceneMap.Keys)
            {
                if (!newScenesSet.Contains(activeScene))
                {
                    Addressables.UnloadSceneAsync(RuntimeSceneContainer.activeWorldSceneMap[activeScene]);
                    RuntimeSceneContainer.activeWorldSceneMap.Remove(activeScene);
                }
            }
        }

        private void ChangeUIScene()
        {
            List<SceneMap.UIScene> newScenesSet = new List<SceneMap.UIScene>();

            foreach (var info in uiSceneInfo)
            {
                newScenesSet.Add(info.type);

                if (!RuntimeSceneContainer.activeUISceneMap.ContainsKey(info.type))
                {
                    AssetReference sceneAsset = SceneMap.uiSceneMap[info.type];
                    AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneAsset, LoadSceneMode.Additive, true, info.priority);
                    RuntimeSceneContainer.activeUISceneMap.Add(info.type, handle);
                }
            }

            foreach (var activeScene in RuntimeSceneContainer.activeUISceneMap.Keys)
            {
                if (!newScenesSet.Contains(activeScene))
                {
                    Addressables.UnloadSceneAsync(RuntimeSceneContainer.activeUISceneMap[activeScene]);
                    RuntimeSceneContainer.activeUISceneMap.Remove(activeScene);
                }
            }
        }
    }
}
