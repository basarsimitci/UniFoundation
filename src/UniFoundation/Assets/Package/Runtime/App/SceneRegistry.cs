using JoyfulWorks.UniFoundation.Logging;
using System.Collections.Generic;

namespace JoyfulWorks.UniFoundation.App
{
    public class SceneRegistry
    {
        public const string LogCategory = "SceneRegistry";

        public const int NullSceneId = 0;
        
        private readonly Dictionary<int, string> sceneNames = new Dictionary<int, string>();

        public void AddScene(int sceneId, string sceneName)
        {
            if (sceneNames.ContainsKey(sceneId))
            {
                Log.Output(LogCategory, $"AddScene: Registry already contains scene id {sceneId}.", LogLevel.Error);
                return;
            }

            if (sceneId == NullSceneId)
            {
                Log.Output(LogCategory, $"AddScene: {sceneId} is reserved, please use another value.", LogLevel.Error);
                return;
            }

            sceneNames[sceneId] = sceneName;
        }

        public string GetSceneName(int sceneId)
        {
            return sceneNames.ContainsKey(sceneId) ? sceneNames[sceneId] : null;
        }
    }
}