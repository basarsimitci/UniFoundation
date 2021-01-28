namespace JoyfulWorks.UniFoundation.App
{
    public interface ISceneNavigator
    {
        void GotoScene(int nextScene);
        void GoBack();
    }
}