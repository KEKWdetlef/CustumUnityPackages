using System;
using UnityEngine;

namespace SceneManagement.Misc
{
    [Serializable]
    public struct UISceneInfo
    {
        [SerializeField] public SceneMap.UIScene type;
        [SerializeField, Range(0, 100)] public int priority;
    }

    [Serializable]
    public struct WorldSceneInfo
    {
        [SerializeField] public SceneMap.WorldScene type;
        [SerializeField, Range(0, 100)] public int priority;
    }
}

