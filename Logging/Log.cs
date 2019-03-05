using System.Collections.Generic;
using UnityEngine;

namespace UniFoundation.Logging
{
    public static class Log
    {
        private static readonly Dictionary<string, LogLevel> CategoryLogLevels = new Dictionary<string, LogLevel>();

        public static void SetCategoryLogLevel(string logCategory, LogLevel logLevel)
        {
            CategoryLogLevels[logCategory] = logLevel;
        }
        
        public static void Output(string logCategory, string log, LogLevel logLevel = LogLevel.Info)
        {
            if ((CategoryLogLevels.ContainsKey(logCategory) == false) ||
                (CategoryLogLevels.ContainsKey(logCategory) && (logLevel >= CategoryLogLevels[logCategory])))
            {
                switch (logLevel)
                {
                    case LogLevel.Info:
                        Debug.Log(log);
                        break;
                    
                    case LogLevel.Warning:
                        Debug.LogWarning(log);
                        break;
                    
                    case LogLevel.Error:
                        Debug.LogError(log);
                        break;
                }
            }
        }
    }
}
