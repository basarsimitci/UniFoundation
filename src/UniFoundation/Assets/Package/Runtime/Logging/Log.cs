using System.Collections.Generic;
using UnityEngine;

namespace JoyfulWorks.UniFoundation.Logging
{
    public static class Log
    {
        private static readonly Dictionary<string, LogLevel> CategoryLogLevels = new Dictionary<string, LogLevel>();
        private static readonly Dictionary<string, List<ILogOutput>> CategoryOutputs = new Dictionary<string, List<ILogOutput>>();

        public const string AllCategories = "All";

        public static void SetStackTraceLevels()
        {
            Application.SetStackTraceLogType(LogType.Warning, StackTraceLogType.None);
            Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);
        }
        
        public static void SetCategoryLogLevel(string logCategory, LogLevel logLevel)
        {
            CategoryLogLevels[logCategory] = logLevel;
        }

        public static void RegisterLogOutput(ILogOutput output, string logCategory)
        {
            if (CategoryOutputs.ContainsKey(logCategory) == false)
            {
                CategoryOutputs[logCategory] = new List<ILogOutput>();
            }

            List<ILogOutput> outputs = CategoryOutputs[logCategory];
            if (outputs.Contains(output) == false)
            {
                outputs.Add(output);
            }
        }

        public static void UnregisterLogOutput(ILogOutput output, string logCategory)
        {
            List<ILogOutput> outputs = CategoryOutputs.ContainsKey(logCategory) ? CategoryOutputs[logCategory] : null;
            outputs?.Remove(output);
        }
        
        public static void Output(string logCategory, string log, LogLevel logLevel = LogLevel.Info)
        {
            if ((CategoryLogLevels.ContainsKey(logCategory) == false) ||
                (CategoryLogLevels.ContainsKey(logCategory) && (logLevel >= CategoryLogLevels[logCategory])))
            {
                if (CategoryOutputs.ContainsKey(AllCategories))
                {
                    foreach (ILogOutput output in CategoryOutputs[AllCategories])
                    {
                        output.OutputLog(logCategory, log, logLevel);
                    }
                }

                if (CategoryOutputs.ContainsKey(logCategory))
                {
                    foreach (ILogOutput output in CategoryOutputs[logCategory])
                    {
                        output.OutputLog(logCategory, log, logLevel);
                    }
                }
            }
        }
    }
}
