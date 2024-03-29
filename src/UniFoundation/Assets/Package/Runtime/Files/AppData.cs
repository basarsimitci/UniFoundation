using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace JoyfulWorks.UniFoundation.Files
{
    public static class AppData
    {
        public static string GetAbsolutePath(string relativePath)
        {
            return Path.Combine(Application.streamingAssetsPath, relativePath);
        }
        
        public static byte[] ReadBinary(string relativePath)
        {
            string absolutePath = GetAbsolutePath(relativePath);
            
#if UNITY_ANDROID
            UnityWebRequest webRequest = UnityWebRequest.Get(absolutePath);
            webRequest.SendWebRequest();
            while (webRequest.downloadHandler.isDone == false)
            {
                // Wait for the task to complete.
            }
            
            return webRequest.downloadHandler.data;
#else
            return FileIO.ReadBinary(absolutePath);
#endif
        }

        public static void ReadBinary(string relativePath, long fileOffset, byte[] buffer, int bufferOffset, int numberOfBytes)
        {
            FileIO.ReadBinary(GetAbsolutePath(relativePath), fileOffset, buffer, bufferOffset, numberOfBytes);
        }

        public static string ReadText(string relativePath)
        {
            string absolutePath = GetAbsolutePath(relativePath);

#if UNITY_ANDROID
            UnityWebRequest webRequest = UnityWebRequest.Get(absolutePath);
            webRequest.SendWebRequest();
            while (webRequest.downloadHandler.isDone == false)
            {
                // Wait for the task to complete.
            }
            
            return webRequest.downloadHandler.text;
#else
            return FileIO.ReadText(absolutePath);
#endif
        }
    }
}