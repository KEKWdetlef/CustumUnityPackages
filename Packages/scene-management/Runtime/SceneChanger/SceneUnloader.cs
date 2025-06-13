using UnityEngine;
using UnityEngine.AddressableAssets;

namespace KekwDetlef.SceneManagement.Runtime
{
    public class SceneUnloader : MonoBehaviour
    {
        [SerializeField] private SceneMap.UIScene[] uiScene;
        [SerializeField] private SceneMap.WorldScene[] worldScene;

        public void UnloadScenes()
        {
            
            UnloadUIScenes();
            UnloadWorldScenes();
        }

        private void UnloadWorldScenes()
        {
            foreach (var sceneInfo in worldScene)
            {
                if (RuntimeSceneContainer.activeWorldSceneMap.ContainsKey(sceneInfo))
                {
                    Addressables.UnloadSceneAsync(RuntimeSceneContainer.activeWorldSceneMap[sceneInfo]);
                    RuntimeSceneContainer.activeWorldSceneMap.Remove(sceneInfo);
                }
            }
        }

        private void UnloadUIScenes()
        {
            foreach (var sceneInfo in uiScene)
            {
                if (RuntimeSceneContainer.activeUISceneMap.ContainsKey(sceneInfo))
                {
                    Addressables.UnloadSceneAsync(RuntimeSceneContainer.activeUISceneMap[sceneInfo]);
                    RuntimeSceneContainer.activeUISceneMap.Remove(sceneInfo);
                }
            }
        }
    }
}
