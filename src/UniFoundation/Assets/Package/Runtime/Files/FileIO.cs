using Cysharp.Threading.Tasks;
using System;
using System.IO;
using System.Text;

namespace JoyfulWorks.UniFoundation.Files
{
    public static class FileIO
    {
        public const string LogCategory = "FileIO";
        
        public static byte[] ReadBinary(string absolutePath)
        {
#if UNITY_WSA
            return UnityEngine.Windows.File.ReadAllBytes(absolutePath);
#else
            return File.ReadAllBytes(absolutePath);
#endif
        }

        public static async UniTask<byte[]> ReadBinaryAsync(string absolutePath)
        {
            int numberOfBytesRead;
            byte[] buffer = new byte[10000000];
                
            using (FileStream fs = File.OpenRead(absolutePath))
            {
                numberOfBytesRead = await fs.ReadAsync(buffer, 0, 10000000);
                fs.Close();
            }
                
            byte[] bytesRead = new byte[numberOfBytesRead];
            Buffer.BlockCopy(buffer, 0, bytesRead, 0, numberOfBytesRead);

            return bytesRead;
        }

        public static void ReadBinary(string absolutePath, long fileOffset, byte[] buffer, int bufferOffset, int numberOfBytes)
        {
            using (FileStream fs = File.OpenRead(absolutePath))
            {
                if (fileOffset > 0)
                {
                    fs.Seek(fileOffset, SeekOrigin.Begin);
                }
                fs.Read(buffer, bufferOffset, numberOfBytes);
                fs.Close();
            }
        }
        
        public static void WriteBinary(string absolutePath, byte[] data)
        {
#if UNITY_WSA
            UnityEngine.Windows.File.WriteAllBytes(absolutePath, data);
#else
            File.WriteAllBytes(absolutePath, data);
#endif
        }

        public static string ReadText(string absolutePath)
        {
            return Encoding.UTF8.GetString(ReadBinary(absolutePath));
        }

        public static void WriteText(string absolutePath, string data)
        {
            WriteBinary(absolutePath, Encoding.UTF8.GetBytes(data));
        }        
    }
}