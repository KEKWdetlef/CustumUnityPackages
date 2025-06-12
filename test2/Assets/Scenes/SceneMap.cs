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
        ,SAMPLESCENE1 = 1
        ,SAMPLESCENE2 = 2
        ,SAMPLESCENE3 = 3
        }

       public enum WorldScene
        {
          NONE = 0
        }

       public static readonly Dictionary<UIScene, AssetReference> uiSceneMap = new()
      {
           { UIScene.NONE, null }
        ,{ UIScene.SAMPLESCENE1, new("2efa1f7ee69934c4b8b8e8954110b1f6") }
        ,{ UIScene.SAMPLESCENE2, new("1b35eb6c17a89470c9ab7ff2c939cb44") }
        ,{ UIScene.SAMPLESCENE3, new("b641aad6ee7df40fbbdf0cc0e0ec66c3") }
        };

      public static readonly Dictionary<WorldScene, AssetReference> worldSceneMap = new()
       {
           { WorldScene.NONE, null }
       };
   }
    }