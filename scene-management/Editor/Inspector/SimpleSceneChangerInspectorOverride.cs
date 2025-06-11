using SceneManagement.Runtime;
using UnityEditor;

namespace SceneManagement.Editor
{
    [CustomEditor(typeof(SimpleSceneChanger))]
    public class SimpleSceneChangerInspectorOverride : UnityEditor.Editor
    {
        SerializedProperty sceneTypeProp, uiSceneInfoProp, worldSceneInfoProp, loadModeProp;

        void OnEnable()
        {
            SimpleSceneChanger me = (SimpleSceneChanger)target;

            sceneTypeProp = serializedObject.FindProperty(me.NO_sceneType);
            loadModeProp = serializedObject.FindProperty(me.NO_loadMode);
            uiSceneInfoProp = serializedObject.FindProperty(me.NO_uiSceneInfo);
            worldSceneInfoProp = serializedObject.FindProperty(me.NO_worldSceneInfo);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(sceneTypeProp);
            EditorGUILayout.PropertyField(loadModeProp);

            if ((SimpleSceneChanger.SceneTypeToLoad)sceneTypeProp.enumValueIndex == SimpleSceneChanger.SceneTypeToLoad.UI)
            {
                EditorGUILayout.PropertyField(uiSceneInfoProp);
            }
            else
            {
                EditorGUILayout.PropertyField(worldSceneInfoProp);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}


