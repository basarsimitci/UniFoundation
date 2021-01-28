using UnityEngine;

namespace JoyfulWorks.UniFoundation.Logging
{
    public class ConsoleLogOutput : ILogOutput
    {
        public void OutputLog(string logCategory, string log, LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Info:
                    Debug.Log($"{logCategory}: {log}");
                    break;
                    
                case LogLevel.Warning:
                    Debug.LogWarning($"{logCategory}: {log}");
                    break;
                    
                case LogLevel.Error:
                    Debug.LogError($"{logCategory}: {log}");
                    break;
            }
        }
    }
}