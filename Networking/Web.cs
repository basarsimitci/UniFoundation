using System;
using UniFoundation.Files;
using UniFoundation.Logging;
using UniRx.Async;
using UnityEngine.Networking;

namespace UniFoundation.Networking
{
    public class Web
    {
        public const string LogCategory = "Web";
        
        public static async UniTask<string> DownloadText(string url, Action<float> progressCallback = null)
        {
            UnityWebRequest webRequest = await Download(url, progressCallback);
            if (webRequest == null)
            {
                Log.Output(LogCategory, $"Web.DownloadText: Could not download from {url}.", LogLevel.Error);
                return null;
            }
            
            Log.Output(LogCategory, $"Web.DownloadText: Downloaded {url}.");

            string text = webRequest.downloadHandler.text.Substring(0, webRequest.downloadHandler.text.Length);
            webRequest.Dispose();

            return text;
        }

        public static async UniTask<byte[]> DownloadBinary(string url, Action<float> progressCallback = null)
        {
            UnityWebRequest webRequest = await Download(url, progressCallback);
            if (webRequest == null)
            {
                Log.Output(LogCategory, $"Web.DownloadBinary: Could not download from {url}.", LogLevel.Error);
                return null;
            }
            
            Log.Output(LogCategory, $"Web.DownloadBinary: Downloaded {url}.");
            
            byte[] data = new byte[webRequest.downloadHandler.data.Length];
            webRequest.downloadHandler.data.CopyTo(data, 0);
            webRequest.Dispose();

            return data;
        }

        public static async UniTask<bool> DownloadFile(string url, string filePath, Action<float> progressCallback = null)
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(url);
            webRequest.downloadHandler = new DownloadHandlerFile(PersistentData.GetAbsolutePath(filePath));
            bool result = await SendWebRequest(webRequest, progressCallback);
            webRequest.Dispose();

            return result;
        }
        
        private static async UniTask<UnityWebRequest> Download(string url, Action<float> progressCallback = null)
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(url);
            bool result = await SendWebRequest(webRequest, progressCallback);
            return result ? webRequest : null;
        }

        private static async UniTask<bool> SendWebRequest(UnityWebRequest request, Action<float> progressCallback = null)
        {
            await request.SendWebRequest().ConfigureAwait(Progress.Create(progressCallback));

            if (request.isNetworkError)
            {
                Log.Output(LogCategory, $"Web.Download: There was a network error while trying to download from {request.url}.",
                    LogLevel.Error);
                return false;
            }

            if (request.isHttpError)
            {
                Log.Output(LogCategory, $"Web.Download: WebRequest resulted in {request.responseCode}: {request.error}",
                    LogLevel.Error);
                return false;
            }
                
            return true;
        }
    }
}