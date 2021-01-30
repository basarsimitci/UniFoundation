/*
using JoyfulWorks.UniFoundation.Input;
using JoyfulWorks.UniFoundation.Logging;
using System;

namespace [Namespace]
{
    public class [ClassName] : InputHub, Test.ISomeInput
    {
        // [Events]

        public override void RegisterInput(IInput input)
        {
            if (Inputs.Contains(input) == false)
            {
                base.RegisterInput(input);

                // [RegisterCalls]
            }
        }

        public override void UnregisterInput(IInput input)
        {
            if (Inputs.Contains(input))
            {
                base.UnregisterInput(input);

                // [UnregisterCalls]
            }
        }
        
        private void Invoke[InputEvent]([InputEventParameters])
        {
            [InputEvent]?.Invoke([InputEventArguments]);
        }

        private void Register[InputType](IInput input)
        {
            if (input is [InputType] [InputType]Input)
            {
                Log.Output(LogCategory, $"{input.Name} registered as [InputType]");

                [InputType]Input.[InputEvent] += Invoke[InputEvent];
            }
        }

        private void Unregister[InputType](IInput input)
        {
            if (input is [InputType] [InputType]Input)
            {
                Log.Output(LogCategory, $"{input.Name} unregistered as [InputType]");

                [InputType]Input.[InputEvent] -= Invoke[InputEvent];
            }
        }
    }
}
*/