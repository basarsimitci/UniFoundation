using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UniFoundation.Editor
{
    public class RenameSelected : EditorWindow
    {
        private string prefix;
        private bool customSuffix;
        private string suffix;
        
        private const string ActionText = "Rename Selected Objects";

        [MenuItem("Edit/" + ActionText)]
        private static void Init()
        {
            RenameSelected window = (RenameSelected) GetWindow(typeof(RenameSelected), true, ActionText);
            window.Show();
        }


        private void OnGUI()
        {
            prefix = EditorGUILayout.TextField("Prefix: ", prefix);

            using (EditorGUILayout.ToggleGroupScope suffixGroup = new EditorGUILayout.ToggleGroupScope("Custom Suffix", customSuffix))
            {
                customSuffix = suffixGroup.enabled;
                suffix = EditorGUILayout.TextField("Suffix: ", suffix);
            }
            
            EditorGUILayout.Space();

            if (GUILayout.Button("Rename", GUILayout.Height(30)))
            {
                Undo.RecordObjects(Selection.transforms, ActionText);
                int objectNo = 1;
                foreach (Transform transform in Selection.transforms.OrderBy(t => t.GetSiblingIndex()))
                {
                    transform.name = customSuffix ? prefix + suffix : prefix + objectNo;
                    objectNo++;
                }
            }
        }
    }
}
