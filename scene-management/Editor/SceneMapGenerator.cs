#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utils.Serializable;

namespace SceneManagement.Editor
{
    // This Class uses a Editor ovverride script.
    [CreateAssetMenu(fileName = "SceneMapGenerator", menuName = "Scriptable Objects/SceneMap/SceneMapGenerator")]
    public class SceneMapGenerator : ScriptableObject
    {
        public List<IndexedScene> uiScenes = new(), worldScenes = new();
        public SerializableDictionary<string, int> rememberedGUID = new();
        public SceneAsset rootScene;
        public string directoryPathSceneMap;
        public int uniqueWorldIdentifier = 0, uniqueUIIdentifier = 0;
    }
}

#endif // UNITY_EDITOR