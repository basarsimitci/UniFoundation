using JoyfulWorks.UniFoundation.Input;
using System;

namespace Test
{
    public interface ISomeInput : IInput
    {
        event Action SomethingHappened;
        event Action<int> IntHappened;
        
    }
}