using JoyfulWorks.UniFoundation.Input;
using System;

namespace JoyfulWorks.UniFoundation.App
{
    public interface IAppLifetimeInput : IInput
    {
        event Action AppLostFocus;
        event Action AppGainedFocus;
        event Action AppPaused;
        event Action AppResumed;
        event Action AppEnding;
    }
}