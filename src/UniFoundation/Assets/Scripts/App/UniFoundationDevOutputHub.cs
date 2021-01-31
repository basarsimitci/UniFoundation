using JoyfulWorks.UniFoundation.Output;

namespace JoyfulWorks.UniFoundationDev.App
{
    public class UniFoundationDevOutputHub : OutputHub,
        JoyfulWorks.UniFoundationDev.Test.ISomeOtherOutput,
        JoyfulWorks.UniFoundationDev.Test.ISomeOutput,
        JoyfulWorks.UniFoundation.Logging.ILogOutput
    {
        #region JoyfulWorks.UniFoundationDev.Test.ISomeOtherOutput

        public void OutputWithGenericParameter(System.Collections.Generic.IEnumerable<System.Int32> intCollection)
        {
            FindOutputsOfType<JoyfulWorks.UniFoundationDev.Test.ISomeOtherOutput>()?.ForEach(output => output.OutputWithGenericParameter(intCollection));
        }

        #endregion

        #region JoyfulWorks.UniFoundationDev.Test.ISomeOutput

        public void Signal()
        {
            FindOutputsOfType<JoyfulWorks.UniFoundationDev.Test.ISomeOutput>()?.ForEach(output => output.Signal());
        }

        public void OutputInt(System.Int32 intValue)
        {
            FindOutputsOfType<JoyfulWorks.UniFoundationDev.Test.ISomeOutput>()?.ForEach(output => output.OutputInt(intValue));
        }

        #endregion

        #region JoyfulWorks.UniFoundation.Logging.ILogOutput

        public void OutputLog(System.String logCategory, System.String log, JoyfulWorks.UniFoundation.Logging.LogLevel logLevel)
        {
            FindOutputsOfType<JoyfulWorks.UniFoundation.Logging.ILogOutput>()?.ForEach(output => output.OutputLog(logCategory, log, logLevel));
        }

        #endregion
    }
}