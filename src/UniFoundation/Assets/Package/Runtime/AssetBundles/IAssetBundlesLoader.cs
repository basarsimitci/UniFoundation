using System;

namespace JoyfulWorks.UniFoundation.AssetBundles
{
    public interface IAssetBundlesLoader
    {
        void LoadAssetBundles(AssetBundlesConfig config);
        event Action<string, float, float> LoadProgress;  // asset bundle name, progress, overall progress
        event Action LoadComplete;
    }
}