using UnityEditor;
using UnityEngine;

namespace JoyfulWorks.UniFoundation.Editor.Package.Editor.Scene
{
    public class MoveSelected : EditorWindow
    {
        private Vector3 delta;

        private const string ActionText = "Move Selected Objects";

        [MenuItem("Edit/" + ActionText)]
        private static void Init()
        {
            MoveSelected window = (MoveSelected) GetWindow(typeof(MoveSelected), true, ActionText);
            window.Show();
        }

        private void OnGUI()
        {
            delta = EditorGUILayout.Vector3Field("Move By: ", delta);

            EditorGUILayout.Space();

            if (GUILayout.Button("Move", GUILayout.Height(30)))
            {
                Undo.RecordObjects(Selection.transforms, ActionText);
                foreach (Transform transform in Selection.transforms)
                {
                    transform.position = transform.position + delta;
                }
            }
        }
    }
}
