using JoyfulWorks.UniFoundation.Input;
using System;
using UnityEngine;

namespace JoyfulWorks.UniFoundationDev.Test
{
    public interface ISomeOtherInput : IInput
    {
        event Action<float> FloatHappened;
        event Action<Vector3> PositionChanged;
    }
}