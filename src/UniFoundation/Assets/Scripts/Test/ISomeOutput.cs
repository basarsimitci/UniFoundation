using JoyfulWorks.UniFoundation.Output;

namespace JoyfulWorks.UniFoundationDev.Test
{
    public interface ISomeOutput : IOutput
    {
        void Signal();
        void OutputInt(int intValue);
    }
}