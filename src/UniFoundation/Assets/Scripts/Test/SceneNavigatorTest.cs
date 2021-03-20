using Cysharp.Threading.Tasks;
using JoyfulWorks.UniFoundation.App;
using JoyfulWorks.UniFoundationDev.App;
using System.Threading;
using UnityEngine;

namespace JoyfulWorks.UniFoundationDev.Test
{
    public class SceneNavigatorTest : MonoBehaviour
    {
        private ISceneNavigator sceneNavigator;
        
        private void Awake()
        {
            DontDestroyOnLoad(this);

            sceneNavigator = UniFoundationDevApp.Instance.SceneNavigator;

            UniTask.Void(CycleScenes, CancellationToken.None);
        }

        private async UniTaskVoid CycleScenes(CancellationToken cancellationToken)
        {
            await UniTask.Delay(1000, cancellationToken: cancellationToken);
            sceneNavigator.GotoScene((int) Scene.Test1);
            await UniTask.Delay(1000, cancellationToken: cancellationToken);
            sceneNavigator.GotoScene((int) Scene.Test2);
            await UniTask.Delay(1000, cancellationToken: cancellationToken);
            sceneNavigator.GoBack();
            await UniTask.Delay(1000, cancellationToken: cancellationToken);
            sceneNavigator.GoBack();
        }
    }
}
