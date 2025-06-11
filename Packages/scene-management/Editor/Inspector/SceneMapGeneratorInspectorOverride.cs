using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utils.Serializable;

namespace SceneManagement.Editor
{
    [CustomEditor(typeof(SceneMapGenerator))]
    public class SceneMapGeneratorInspectorOverride : UnityEditor.Editor
    {
        public enum SceneType
        {
            UI, WORLD
        }
        public const string sceneMapFileName = "SceneMap.cs";
        private bool shouldSetDirty;
        private bool _showScenes = true, _showUIScenes = true, _showWorldScenes = true, _showRememberedScenes = false;
        private bool _thisSceneChangeEnabled, _removeScenesEnabled, _clearOrResetIdentifierEnabled;
        private int selectedItem = 0;
        
        private SceneType selectedSceneType;

        public override void OnInspectorGUI()
        {
            SceneMapGenerator me = (SceneMapGenerator)target;

            if (RemoveNullItems(ref me.uiScenes))
            { shouldSetDirty = true; }

            if (RemoveNullItems(ref me.worldScenes))
            { shouldSetDirty = true; }

            DisplayScenes(me.uiScenes, me.worldScenes);

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();


            if (AddCheck("Add UI Scene: ", ref me.uiScenes, ref me.uniqueUIIdentifier, me.rootScene, ref me.rememberedGUID))
            { shouldSetDirty = true; }

            if (AddCheck("Add World Scene", ref me.worldScenes, ref me.uniqueWorldIdentifier, me.rootScene, ref me.rememberedGUID))
            { shouldSetDirty = true; }

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            _removeScenesEnabled = EditorGUILayout.ToggleLeft("Enable to Remove Scenes", _removeScenesEnabled);
            EditorGUI.BeginDisabledGroup(!_removeScenesEnabled);
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.LabelField("Remove item at index");
            EditorGUILayout.BeginHorizontal();
            selectedItem = EditorGUILayout.IntField(selectedItem);
            selectedSceneType = (SceneType)EditorGUILayout.EnumPopup(selectedSceneType);
            EditorGUILayout.EndHorizontal();

            EditorGUI.EndChangeCheck();

            if (GUILayout.Button("Remove Item"))
            {
                switch (selectedSceneType)
                {
                    case SceneType.WORLD:
                        RemoveItemAt(ref me.worldScenes, selectedItem);
                        break;
                    case SceneType.UI:
                        RemoveItemAt(ref me.uiScenes, selectedItem);
                        break;
                }
            }
            EditorGUI.EndDisabledGroup();


            EditorGUILayout.Space();
            EditorGUILayout.Space();


            EditorGUILayout.LabelField(me.directoryPathSceneMap);
            if (GUILayout.Button("Select Directory"))
            {
                string filePathSceneMap = EditorUtility.OpenFolderPanel("Choose Folder", "", "");
                if (!string.IsNullOrEmpty(filePathSceneMap))
                {
                    // Save path relative to project if it's inside Assets
                    if (filePathSceneMap.StartsWith(Application.dataPath))
                    {
                        filePathSceneMap = "Assets" + filePathSceneMap.Substring(Application.dataPath.Length);
                        filePathSceneMap += "/";
                    }

                    me.directoryPathSceneMap = filePathSceneMap;
                    shouldSetDirty = true;
                }
            }

            if (GUILayout.Button("Cook Scene Map"))
            {
                CookSceneMap(me.uiScenes, me.worldScenes, me.directoryPathSceneMap);
            }
            EditorGUILayout.HelpBox("ALPHA VERSION MAY LEAD TO FUNNY", MessageType.Warning);


            EditorGUILayout.Space();
            EditorGUILayout.Space();


            _thisSceneChangeEnabled = EditorGUILayout.ToggleLeft("Enable Change RootGame.", _thisSceneChangeEnabled);
            EditorGUI.BeginDisabledGroup(!_thisSceneChangeEnabled);

            EditorGUI.BeginChangeCheck();
            me.rootScene = (SceneAsset)EditorGUILayout.ObjectField("The Bootstrap Scene", me.rootScene, typeof(SceneAsset), false);
            EditorGUILayout.HelpBox("Dont Change this unless you know exactly what you are doing!!", MessageType.Warning);
            if (EditorGUI.EndChangeCheck())
            {
                shouldSetDirty = true;
            }

            EditorGUI.EndDisabledGroup();

            _clearOrResetIdentifierEnabled = EditorGUILayout.ToggleLeft("Enabe to remove scene and or reset unique identifier", _clearOrResetIdentifierEnabled);
            EditorGUI.BeginDisabledGroup(!_clearOrResetIdentifierEnabled);
            if (GUILayout.Button("Clear Scenes"))
            {
                me.worldScenes.Clear();
                me.uiScenes.Clear();
                me.rememberedGUID.Clear();

                shouldSetDirty = true;
            }

            if (GUILayout.Button("Resert Unique Identifier."))
            {
                me.uniqueWorldIdentifier = 0;
                me.uniqueUIIdentifier = 0;
                shouldSetDirty = true;
            }

            EditorGUI.EndDisabledGroup();

            _showRememberedScenes = EditorGUILayout.Foldout(_showRememberedScenes, "Remembered Scenes");
            if (_showRememberedScenes)
            {
                EditorGUI.indentLevel++;

                EditorGUI.BeginDisabledGroup(true);

                foreach (KeyValuePair<string, int> pair in me.rememberedGUID)
                {
                    SceneAsset obj = AssetDatabase.LoadAssetAtPath<SceneAsset>(AssetDatabase.GUIDToAssetPath(pair.Key));

                    EditorGUILayout.ObjectField(obj, typeof(AssetReference), false);
                }

                EditorGUI.EndDisabledGroup();

                EditorGUI.indentLevel--;
            }

            if (shouldSetDirty)
                {
                    EditorUtility.SetDirty(me);
                }
        }

        private bool RemoveItemAt(ref List<IndexedScene> scenesArray, int selectedItem)
        {
            if (scenesArray.Count <= selectedItem)
            {
                Debug.LogWarning("List is to short to contian that item.");
                return false;
            }

            scenesArray.RemoveAt(selectedItem);
            return true;
        }

        private void CookSceneMap(List<IndexedScene> uiScenes, List<IndexedScene> worldScenes, string dictionaryPath)
        {
            Debug.LogError(dictionaryPath);
            if (dictionaryPath == string.Empty)
            {
                Debug.Log("Select Path to generate to");
                return;
            }
            string filePath = dictionaryPath + sceneMapFileName;
            
            string uiSceneEnumStringFormated = FormatToEnumString(uiScenes);
            string uiSceneDictionaryStringFormated = FormatToDictionaryString(uiScenes, SceneType.UI);

            string worldSceneEnumStringFormated = FormatToEnumString(worldScenes);
            string worldSceneDictionaryStringFormated = FormatToDictionaryString(worldScenes, SceneType.WORLD);

            StringBuilder final = new StringBuilder();

            final.Append(SceneMapTemplate.FinishedSceneMapString(uiSceneEnumStringFormated, uiSceneDictionaryStringFormated, worldSceneEnumStringFormated, worldSceneDictionaryStringFormated));

            if (File.Exists(filePath))
            {
                string oldSceneMap = File.ReadAllText(filePath);

                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                final.AppendLine();
                final.AppendLine($"// ----- Previous SceneMap [Deprecated scince {timestamp}] -----");

                using (var reader = new StringReader(oldSceneMap))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        final.AppendLine("// " + line);
                    } 
                }
            }

            using (StreamWriter sw = new(filePath, false))
            {
                sw.Write(final);
            }

            AssetDatabase.Refresh();
        }

        private string FormatToDictionaryString(List<IndexedScene> indexedScenes, SceneType type)
        {
            string enumName = string.Empty;
            switch (type)
            {
                case SceneType.UI:
                    enumName = "UIScene";
                    break;
                case SceneType.WORLD:
                    enumName = "WorldScene";
                    break;
            }

            StringBuilder final = new StringBuilder();
            final.Append("{ " + enumName + ".NONE, null }");

            foreach (IndexedScene indexedScene in indexedScenes)
            {
                string sceneName = indexedScene.scene.editorAsset.name.ToUpper();

                final.Append("\n        ,{ " + enumName + "." + sceneName + ", new(" + '"' + indexedScene.scene.AssetGUID + '"' + ") }");
            }

            return final.ToString();
        }

        private string FormatToEnumString(List<IndexedScene> indexedScenes)
        {
            StringBuilder final = new StringBuilder();
            final.Append("NONE = 0");

            foreach (IndexedScene indexedScene in indexedScenes)
            {
                string sceneName = indexedScene.scene.editorAsset.name.ToUpper();
                final.Append($"\n        ,{sceneName} = {indexedScene.sceneIndex}");
            }

            return final.ToString();
        }

        private bool RemoveNullItems(ref List<IndexedScene> sceneArray)
        {
            int removedCount = sceneArray.RemoveAll(indexedScene => indexedScene.scene.editorAsset == null);
            return removedCount > 0;
        }

        private bool AddCheck(string text, ref List<IndexedScene> scenesArray, ref int lastIndex, SceneAsset rootScene, ref SerializableDictionary<string, int> rememberedGUID)
        {
            EditorGUI.BeginChangeCheck();
            bool anyAdded = AddScene(text, ref scenesArray, ref lastIndex, rootScene, ref rememberedGUID);
            EditorGUI.EndChangeCheck();

            return anyAdded;
        }

        private bool AddScene(string text, ref List<IndexedScene> scenesArray, ref int lastIndex, SceneAsset rootScene, ref SerializableDictionary<string, int> rememberedGUID)
        {
            SceneAsset newScene = (SceneAsset)EditorGUILayout.ObjectField(text, null, typeof(SceneAsset), false);
            if (newScene == null)
            { return false; }

            string newScenePath = AssetDatabase.GetAssetPath(newScene);
            string newSceneGUID = AssetDatabase.AssetPathToGUID(newScenePath);

            if (IsDupplicateOrThis(scenesArray, newSceneGUID, rootScene))
            { return false; }

            
            int index;
            if (rememberedGUID.ContainsKey(newSceneGUID))
            {
                index = rememberedGUID[newSceneGUID];
                Debug.Log($"Remembererd Item: '{newSceneGUID}' and set the index back to {index}");
            }
            else
            {
                lastIndex++;
                index = lastIndex;
                rememberedGUID.Add(newSceneGUID, index);
                Debug.Log($"Added new scene {newSceneGUID} with an index of {index}");
            }

            AssetReference newSceneReference = new(newSceneGUID);

            scenesArray.Add(new(newSceneReference, index));
            return true;
        }

        private bool IsDupplicateOrThis(List<IndexedScene> scenesArray, string newSceneGUID, SceneAsset rootScene)
        {
            if (rootScene == null)
            {
                Debug.LogWarning("The Root Scenene Must be known to the Game.");
                return true;
            }

            string rootScenePath = AssetDatabase.GetAssetPath(rootScene);
            if (newSceneGUID.Equals(AssetDatabase.AssetPathToGUID(rootScenePath)))
            {
                Debug.LogWarning("Tryed to add the root (Bootstrap) scene to the scne list, which you shouldnt do.");
                return true;
            }

            foreach (IndexedScene indexedScene in scenesArray)
            {
                AssetReference sceneReference = indexedScene.scene;

                if (sceneReference.AssetGUID == newSceneGUID)
                {
                    Debug.LogWarning("Tryed to add same item twice");
                    return true;
                }
            }

            return false;
        }

        private void DisplayScenes(List<IndexedScene> lastUIScenes, List<IndexedScene> lastWorldScenes)
        {
            _showScenes = EditorGUILayout.Foldout(_showScenes, "Scenes");
            if (!_showScenes)
            { return; }

            EditorGUI.indentLevel++;

            DisplayEach("UI Scenes", lastUIScenes, ref _showUIScenes);
            DisplayEach("World Scenes", lastWorldScenes, ref _showWorldScenes);

            EditorGUI.indentLevel--;
        }

        private void DisplayEach(string text, List<IndexedScene> lastScenes, ref bool show)
        {
            show = EditorGUILayout.Foldout(show, text);
            if (!show)
            { return; }

            EditorGUI.indentLevel++;
            EditorGUI.BeginDisabledGroup(true);

            if (lastScenes != null)
            {
                int index = 0;
                foreach (IndexedScene indexedScene in lastScenes)
                {
                    UnityEngine.Object obj = indexedScene.scene.editorAsset;
                    EditorGUILayout.ObjectField($"Index: {index}: ", obj, typeof(AssetReference), false);
                    index++;
                }
            }

            EditorGUI.EndDisabledGroup();
            EditorGUI.indentLevel--;
        }
    }
}

