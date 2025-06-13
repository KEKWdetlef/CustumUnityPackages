// This File is Automaticaly Generated. If you modify this file it will most likely be overwritten. 

using System.Collections.Generic; 
using UnityEngine.AddressableAssets;

namespace KekwDetlef.SceneManagemet.Runtime
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
        }

        public enum WorldScene
        {
           NONE = 0
        }

        public static readonly Dictionary<UIScene, AssetReference> uiSceneMap = new()
        {
            { UIScene.NONE, null }
            ,{ UIScene.SAMPLESCENE_1, new("f4d65aa79c45a41339643d106e08617b") }
            ,{ UIScene.SAMPLESCENE_2, new("3a347ee97cd93405285bbab72cec1876") }
            ,{ UIScene.SAMPLESCENE_3, new("5eb7f44fcea49459da153016d9dd4f0e") }
            ,{ UIScene.SAMPLESCENE_4, new("0c39c8ae6b8254ddeb99682f9199c793") }
        };

       public static readonly Dictionary<WorldScene, AssetReference> worldSceneMap = new()
        {
            { WorldScene.NONE, null }
        };
    }
}
// ----- Previous SceneMap [Deprecated scince 2025-06-13 13:11:40] -----
// 
// // This File is Automaticaly Generated. If you modify this file it will most likely be overwritten. 
// 
// using System.Collections.Generic; 
// using UnityEngine.AddressableAssets;
// 
// namespace KekwDetlef.SceneManagemet.Runtime
// {
//     public static class SceneMap
//     {
//         public enum UIScene
//         {
//            NONE = 0
//         }
// 
//         public enum WorldScene
//         {
//            NONE = 0
//         }
// 
//         public static readonly Dictionary<UIScene, AssetReference> uiSceneMap = new()
//         {
//             { UIScene.NONE, null }
//         };
// 
//        public static readonly Dictionary<WorldScene, AssetReference> worldSceneMap = new()
//         {
//             { WorldScene.NONE, null }
//         };
//     }
// }
