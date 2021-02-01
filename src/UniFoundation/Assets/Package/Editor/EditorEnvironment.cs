using JoyfulWorks.UniFoundation.Editor.Config;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace JoyfulWorks.UniFoundation.Editor
{
    [InitializeOnLoad]
    public class EditorEnvironment
    {
        private const string EditorConfigFolder = "Assets/UniFoundation/Editor";

        public static UniFoundationConfig EditorConfig;
        private const string EditorConfigFile = "config.asset";
        
        static EditorEnvironment()
        {
            LoadEditorConfig();
        }

        private static void LoadEditorConfig()
        {
            string filePath = Path.Combine(EditorConfigFolder, EditorConfigFile);
            EditorConfig = AssetDatabase.LoadAssetAtPath<UniFoundationConfig>(filePath);
            if (EditorConfig == null)
            {
                EditorConfig = ScriptableObject.CreateInstance<UniFoundationConfig>();
                Directory.CreateDirectory(EditorConfigFolder);
                AssetDatabase.CreateAsset(EditorConfig, filePath);
            }
        }
    }
}