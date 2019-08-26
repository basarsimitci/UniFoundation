using System.Collections.Generic;
using UniFoundation.Logging;
using UniRx.Async;
using UnityEngine.SceneManagement;

namespace UniFoundation.App
{
    public class SceneNavigator : ISceneNavigator
    {
        public const string LogCategory = "SceneNavigator";

        private readonly SceneRegistry sceneRegistry;
        private readonly Stack<int> sceneStack = new Stack<int>();

        public SceneNavigator(SceneRegistry sceneRegistry)
        {
            this.sceneRegistry = sceneRegistry;
        }

        public async void GotoScene(int nextScene)
        {
            string nextSceneName = sceneRegistry?.GetSceneName(nextScene);
            if (nextSceneName != null)
            {
                Log.Output(LogCategory, $"GotoScene {nextSceneName} start:");
                LogSceneStack();

                int currentScene = sceneStack.Count > 0 ? sceneStack.Peek() : SceneRegistry.NullSceneId;
                if (currentScene != nextScene)
                {
                    string currentSceneName = sceneRegistry.GetSceneName(currentScene);
                    if (currentSceneName != null)
                    {
                        SceneManager.UnloadSceneAsync(currentSceneName);
                    }

                    await SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);
                    sceneStack.Push(nextScene);
                }

                Log.Output(LogCategory, $"GotoScene {nextSceneName} end:");
                LogSceneStack();
            }
        }

        public async void GoBack()
        {
            Log.Output(LogCategory, "GoBack start:");
            LogSceneStack();

            if (sceneStack.Count > 1)
            {
                int currentScene = sceneStack.Pop();
                await SceneManager.UnloadSceneAsync(sceneRegistry.GetSceneName(currentScene));
                await SceneManager.LoadSceneAsync(sceneRegistry.GetSceneName(sceneStack.Peek()), LoadSceneMode.Additive);
            }
            
            Log.Output(LogCategory, "GoBack end:");
            LogSceneStack();
        }

        private void LogSceneStack()
        {
            string scenes = "";
            foreach (int scene in sceneStack)
            {
                scenes = scene + " > " + scenes;
            }

            if (scenes.Length > 3)
            {
                scenes = scenes.Remove(scenes.Length - 3, 3);
            }

            Log.Output(LogCategory, scenes);
        }
    }
}