using JoyfulWorks.UniFoundation.Logging;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

namespace JoyfulWorks.UniFoundation.App
{
    internal class SceneNavigator : ISceneNavigator
    {
        public const string LogCategory = "SceneNavigator";

        private readonly Stack<int> sceneStack = new Stack<int>();

        public SceneNavigator()
        {
            sceneStack.Push(SceneManager.GetActiveScene().buildIndex);
        }

        public void GotoScene(int nextScene)
        {
            Log.Output(LogCategory, $"GotoScene {nextScene} start:");
            LogSceneStack();

            int currentScene = sceneStack.Count > 0 ? sceneStack.Peek() : -1;
            if (currentScene != nextScene)
            {
                SceneManager.LoadScene(nextScene);
                sceneStack.Push(nextScene);
            }

            Log.Output(LogCategory, $"GotoScene {nextScene} end:");
            LogSceneStack();
        }

        public void GoBack()
        {
            Log.Output(LogCategory, "GoBack start:");
            LogSceneStack();

            if (sceneStack.Count > 1)
            {
                sceneStack.Pop();
                SceneManager.LoadScene(sceneStack.Peek());
            }
            
            Log.Output(LogCategory, "GoBack end:");
            LogSceneStack();
        }

        private void LogSceneStack()
        {
            string scenes = sceneStack.Aggregate("", (current, scene) => scene + " > " + current);

            if (scenes.Length > 3)
            {
                scenes = scenes.Remove(scenes.Length - 3, 3);
            }

            Log.Output(LogCategory, scenes);
        }
    }
}