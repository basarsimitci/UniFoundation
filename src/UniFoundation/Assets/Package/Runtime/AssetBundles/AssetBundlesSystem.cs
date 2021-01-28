using Cysharp.Threading.Tasks;
using JoyfulWorks.UniFoundation.Logging;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace JoyfulWorks.UniFoundation.AssetBundles
{
    public class AssetBundlesSystem : IAssetBundlesLoader
    {
        public const string LogCategory = "AssetBundles";
        
        public event Action<string, float, float> LoadProgress;
        public event Action LoadComplete;

        private readonly Dictionary<string, AssetBundle> assetBundles = new Dictionary<string, AssetBundle>();

        public void LoadAssetBundles(AssetBundlesConfig config)
        {
            LoadBundles(config);
        }

        public AssetBundle GetAssetBundle(string name)
        {
            return assetBundles.ContainsKey(name) ? assetBundles[name] : null;
        }

        private async UniTask LoadBundles(AssetBundlesConfig config)
        {
            if (config?.AssetBundleInfos != null)
            {
                float progressPerBundle = 1.0f / config.AssetBundleInfos.Length;
                
                for (int assetBundleIndex = 0; assetBundleIndex < config.AssetBundleInfos.Length; assetBundleIndex++)
                {
                    AssetBundleInfo assetBundleInfo = config.AssetBundleInfos[assetBundleIndex];

                    float progressBase = assetBundleIndex * progressPerBundle;
                    float overallProgress;

                    UnityWebRequest request =
                        UnityWebRequestAssetBundle.GetAssetBundle(assetBundleInfo.Uri, assetBundleInfo.Version, 0);

                    IProgress<float> onProgress = Progress.Create<float>(progress =>
                    {
                        overallProgress = progressBase + (progress * progressPerBundle);
                        ReportLoadProgress(assetBundleInfo.Name, progress, overallProgress);
                    });
                    
                    await request.SendWebRequest().ToUniTask(onProgress);

                    overallProgress = progressBase + progressPerBundle;
                    ReportLoadProgress(assetBundleInfo.Name, 1.0f, overallProgress);
                    assetBundles[assetBundleInfo.Name] = DownloadHandlerAssetBundle.GetContent(request);
                }

                ReportLoadComplete();
            }
        }

        private void ReportLoadProgress(string bundleName, float bundleProgress, float overallProgress)
        {
            Log.Output(LogCategory, $"Loading bundle {bundleName}: {bundleProgress:P1}. Overall progress: {overallProgress:P1}");
            LoadProgress?.Invoke(bundleName, bundleProgress, overallProgress);
        }

        private void ReportLoadComplete()
        {
            Log.Output(LogCategory, $"Finished loading asset bundles.");
            LoadComplete?.Invoke();
        }
    }
}