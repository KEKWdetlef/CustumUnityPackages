// This File is Automaticaly Generated. If you modify this file it will most likely be overwritten. 

using System.Collections.Generic; 
using UnityEngine.AddressableAssets;

namespace KekwDetlef.SceneManagement
{
    public static class SceneMap
    {
        public enum UIScene
        {
           NONE = 0
            ,SAMPLESCENE_1 = 1
            ,SAMPLESCENE_2 = 2
            ,SAMPLESCENE_3 = 3
            ,SAMPLESCENE_4 = 4
            ,SAMPLESCENE_5 = 5
        }

        public enum WorldScene
        {
           NONE = 0
        }

        public static readonly Dictionary<UIScene, AssetReference> uiSceneMap = new()
        {
            { UIScene.NONE, null }
            ,{ UIScene.SAMPLESCENE_1, new("c15f7aeb7ba458d4dad8578fa6002678") }
            ,{ UIScene.SAMPLESCENE_2, new("5c40993247c683445ae0dba8b6cd4363") }
            ,{ UIScene.SAMPLESCENE_3, new("a7ce23613dbe2974a848af2591a0791f") }
            ,{ UIScene.SAMPLESCENE_4, new("8502e924f029c0246905d54b0003c3d4") }
            ,{ UIScene.SAMPLESCENE_5, new("ea271965b0f1ab54a9632224e81a4820") }
        };

       public static readonly Dictionary<WorldScene, AssetReference> worldSceneMap = new()
        {
            { WorldScene.NONE, null }
        };
    }
}