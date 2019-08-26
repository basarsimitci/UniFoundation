using System;

namespace UniFoundation.App
{
    public interface ISceneNavigator
    {
        void GotoScene(int nextScene);
        void GoBack();
    }
}