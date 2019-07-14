using System;
using System.Collections;
using System.Collections.Generic;
using UniFoundation.App;
using UniFoundation.Logging;
using UnityEngine;
using UnityEngine.Networking;

namespace UniFoundation.AssetBundles
{
    public class AssetBundlesSystem : IAssetBundlesLoader
    {
        public const string LogCategory = "AssetBundles";
        
        public event Action<string, float, float> LoadProgress;
        public event Action LoadComplete;

        private readonly ICoroutineRunner coroutineRunner;
        private readonly Dictionary<string, AssetBundle> assetBundles = new Dictionary<string, AssetBundle>();
        
        public AssetBundlesSystem(ICoroutineRunner coroutineRunner)
        {
            this.coroutineRunner = coroutineRunner;
        }

        public void LoadAssetBundles(AssetBundlesConfig config)
        {
            coroutineRunner.StartCoroutine(LoadBundles(config));
        }

        public AssetBundle GetAssetBundle(string name)
        {
            return assetBundles.ContainsKey(name) ? assetBundles[name] : null;
        }

        private IEnumerator LoadBundles(AssetBundlesConfig config)
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

                    UnityWebRequestAsyncOperation asyncOperation = request.SendWebRequest();
                    while (asyncOperation.isDone == false)
                    {
                        overallProgress = progressBase + (asyncOperation.progress * progressPerBundle);
                        ReportLoadProgress(assetBundleInfo.Name, asyncOperation.progress, overallProgress);

                        yield return null;
                    }
                    
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