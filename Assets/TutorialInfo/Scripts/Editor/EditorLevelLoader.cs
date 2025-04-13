using levelsSO;
using scriptableObjects;
using UnityEditor;
using UnityEngine;

namespace TutorialInfo.Scripts.Editor
{
    [CustomEditor(typeof(NodeManager))]
    public class EditorLevelLoader : UnityEditor.Editor
    {
        private int selectedLevelIndex;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var nodeManager = (NodeManager)target;

            nodeManager.levelDatabase = (LevelDatabase)EditorGUILayout.ObjectField("Level Database", nodeManager.levelDatabase, typeof(LevelDatabase), false);

            if (nodeManager.levelDatabase == null || nodeManager.levelDatabase.allLevels.Count == 0)
            {
                EditorGUILayout.HelpBox("Assign a Level Database with levels.", MessageType.Warning);
                return;
            }

            string[] levelNames = nodeManager.levelDatabase.allLevels.ConvertAll(level => level.name).ToArray();
            selectedLevelIndex = EditorGUILayout.Popup("Select Level", selectedLevelIndex, levelNames);

            GUILayout.Space(10);

            if (GUILayout.Button("Load Level in Scene View"))
            {
                LoadLevelInSceneView(nodeManager.levelDatabase.allLevels[selectedLevelIndex]);
            }

            if (GUILayout.Button("Clear Level"))
            {
                ClearLevelInScene();
            }

            EditorUtility.SetDirty(nodeManager);
        }


        private void LoadLevelInSceneView(BaseLevelSo level)
        {
            var nodeManager = (NodeManager)target;

            nodeManager.ClearLevelInEditor();
            nodeManager.LoadLevel(level, firstLoad: true);

            EditorUtility.SetDirty(nodeManager);
        }

        private void ClearLevelInScene()
        {
            NodeManager nodeManager = (NodeManager)target;
            nodeManager.ClearLevelInEditor();
            EditorUtility.SetDirty(nodeManager);
        }
    }
}