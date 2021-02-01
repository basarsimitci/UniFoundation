using UnityEngine;

namespace JoyfulWorks.UniFoundation.Editor.Config
{
    public class UniFoundationConfig : ScriptableObject
    {
        [Header("Code Generation")]
        
        [Tooltip("Folder to place generated App and Hub classes.")]
        public string AppFolder;

        [Tooltip("Prefix for the name of generated App and Hub classes.")]
        public string ClassNamePrefix;
    }
}