using JoyfulWorks.UniFoundation.Output;

namespace JoyfulWorks.UniFoundation.Logging
{
    public interface ILogOutput : IOutput
    {
        void OutputLog(string logCategory, string log, LogLevel logLevel);
    }
}