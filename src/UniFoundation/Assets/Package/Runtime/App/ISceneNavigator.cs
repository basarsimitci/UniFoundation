using Cysharp.Threading.Tasks;

namespace JoyfulWorks.UniFoundation.App
{
    public interface ISceneNavigator
    {
        UniTask LoadScene(string sceneName);
        void GotoScene(int nextScene);
        void GoBack();
    }
}