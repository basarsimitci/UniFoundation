using System.Linq;
using UnityEditor;

namespace JoyfulWorks.UniFoundation.Editor.CodeGeneration.SceneEnum
{
    public class SceneEnumGenerator : TypeGenerator
    {
        private const string Template =
            "namespace [Namespace]\n" +
            "{\n" +
            "    public enum [TypeName]\n" +
            "    {\n" +
            "[SceneNames]" +
            "    }\n" +
            "}";

        public SceneEnumGenerator(string targetFolder) : base("Scene", targetFolder)
        {
        }
  
        public string Generate()
        {
            string sceneNames = EditorBuildSettings.scenes
                .Where(scene => scene.enabled)
                .Aggregate(string.Empty, 
                    (current, scene) => current + $"        {scene.path.Split('/').Last().Replace(".unity", "")},\n");

            sceneNames = sceneNames.Remove(sceneNames.LastIndexOf(','), 1);

            return Template
                .Replace("[Namespace]", Namespace)
                .Replace("[TypeName]", TypeName)
                .Replace("[SceneNames]", sceneNames);
        }
    }
}