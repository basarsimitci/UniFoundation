using JoyfulWorks.UniFoundation.Input;
using System;
using UnityEngine;

namespace Test
{
    public interface ISomeOtherInput : IInput
    {
        event Action<float> FloatHappened;
        event Action OtherThingHappened;
        event Action<Vector3> PositionChanged;
    }
}