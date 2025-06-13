// using UnityEngine;
// using UnityEngine.AddressableAssets;
// using UnityEngine.ResourceManagement.AsyncOperations;
// using UnityEngine.ResourceManagement.ResourceProviders;
// using UnityEngine.SceneManagement;

// namespace KekwDetlef.SceneManagement.Runtime
// {
//     public class ScenePreLoader : MonoBehaviour
//     {
//         [SerializeField] private UISceneInfo[] uiScenesToLoad;
//         [SerializeField] private WorldSceneInfo[] worldScenesToLoad;

//         public void PreLoadScenes()
//         {
//             // TODO: check if already active
//             foreach (UISceneInfo sceneToLoad in uiScenesToLoad)
//             {
//                 AssetReference sceneAsset = SceneMap.uiSceneMap[sceneToLoad.type];
//                 PreLoadScene(sceneAsset, sceneToLoad.priority);
//             }

//             foreach (WorldSceneInfo sceneToLoad in worldScenesToLoad)
//             {
//                 AssetReference sceneAsset = SceneMap.worldSceneMap[sceneToLoad.type];
//                 PreLoadScene(sceneAsset, sceneToLoad.priority);
//             }
//         }

//         public void UnPreLoadScenes()
//         {
//             foreach (UISceneInfo sceneToUnload in uiScenesToLoad)
//             {
//                 AssetReference sceneAsset = SceneMap.uiSceneMap[sceneToUnload.type];
//                 UnPreLoadScene(sceneAsset);
//             }

//             foreach (WorldSceneInfo sceneToUnload in worldScenesToLoad)
//             {
//                 AssetReference sceneAsset = SceneMap.worldSceneMap[sceneToUnload.type];
//                 UnPreLoadScene(sceneAsset);
//             }
//         }

//         private void UnPreLoadScene(AssetReference sceneAsset)
//         {
//             if (!RuntimeSceneContainer.preloadedSceneMap.ContainsKey(sceneAsset))
//             { return; }

//             AsyncOperationHandle<SceneInstance> handle = RuntimeSceneContainer.preloadedSceneMap[sceneAsset];

//             // make sure to only unload if there is something to unload
//             if (handle.IsDone)
//             {
//                 DirectUnload(handle, sceneAsset);
//                 return;
//             }

//             handle.Completed += myHandle => DirectUnload(myHandle, sceneAsset);
//         }

//         private void DirectUnload(AsyncOperationHandle<SceneInstance> handle, AssetReference sceneAsset)
//         {
//             Addressables.UnloadSceneAsync(handle);
//             RuntimeSceneContainer.preloadedSceneMap.Remove(sceneAsset);
//         }

//         private void PreLoadScene(AssetReference sceneAsset, int priority)
//         {
//             if (RuntimeSceneContainer.preloadedSceneMap.ContainsKey(sceneAsset))
//             { return; }

//             AsyncOperationHandle<SceneInstance> loadHandle = Addressables.LoadSceneAsync(sceneAsset, LoadSceneMode.Additive, false, priority);
//             RuntimeSceneContainer.preloadedSceneMap.Add(sceneAsset, loadHandle);
//         }
//     }
// }

