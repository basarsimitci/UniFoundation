using UniFoundation.Output;

namespace UniFoundation.Logging
{
    public interface ILogOutput : IOutput
    {
        void OutputLog(string logCategory, string log, LogLevel logLevel);
    }
}