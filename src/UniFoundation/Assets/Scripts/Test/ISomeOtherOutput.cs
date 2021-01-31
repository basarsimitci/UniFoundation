using JoyfulWorks.UniFoundation.Output;
using System.Collections.Generic;

namespace JoyfulWorks.UniFoundationDev.Test
{
    public interface ISomeOtherOutput : IOutput
    {
        void OutputWithGenericParameter(IEnumerable<int> intCollection);
    }
}