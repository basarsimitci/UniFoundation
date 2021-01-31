using JoyfulWorks.UniFoundation.Input;
using JoyfulWorks.UniFoundation.Logging;

namespace JoyfulWorks.UniFoundationDev.App
{
    public class UniFoundationDevInputHub : InputHub,
        JoyfulWorks.UniFoundationDev.Test.ISomeInput,
        JoyfulWorks.UniFoundationDev.Test.ISomeOtherInput
    {
        public override void RegisterInput(IInput input)
        {
            if (Inputs.Contains(input) == false)
            {
                base.RegisterInput(input);
                RegisterSomeInput(input);
                RegisterSomeOtherInput(input);
            }
        }

        public override void UnregisterInput(IInput input)
        {
            if (Inputs.Contains(input))
            {
                base.UnregisterInput(input);
                UnregisterSomeInput(input);
                UnregisterSomeOtherInput(input);
            }
        }

        #region JoyfulWorks.UniFoundationDev.Test.ISomeInput

        public event System.Action SomethingHappened;
        public event System.Action<System.Int32> IntHappened;

        private void InvokeSomethingHappened()
        {
            SomethingHappened?.Invoke();
        }

        private void InvokeIntHappened(System.Int32 arg1)
        {
            IntHappened?.Invoke(arg1);
        }

        private void RegisterSomeInput(IInput input)
        {
            if (input is JoyfulWorks.UniFoundationDev.Test.ISomeInput typedInput)
            {
                Log.Output(LogCategory, $"{input.Name} registered as ISomeInput");
                typedInput.SomethingHappened += InvokeSomethingHappened;
                typedInput.IntHappened += InvokeIntHappened;
            }
        }

        private void UnregisterSomeInput(IInput input)
        {
            if (input is JoyfulWorks.UniFoundationDev.Test.ISomeInput typedInput)
            {
                Log.Output(LogCategory, $"{input.Name} unregistered as ISomeInput");
                typedInput.SomethingHappened -= InvokeSomethingHappened;
                typedInput.IntHappened -= InvokeIntHappened;
            }
        }

        #endregion

        #region JoyfulWorks.UniFoundationDev.Test.ISomeOtherInput

        public event System.Action<System.Single> FloatHappened;
        public event System.Action<UnityEngine.Vector3> PositionChanged;

        private void InvokeFloatHappened(System.Single arg1)
        {
            FloatHappened?.Invoke(arg1);
        }

        private void InvokePositionChanged(UnityEngine.Vector3 arg1)
        {
            PositionChanged?.Invoke(arg1);
        }

        private void RegisterSomeOtherInput(IInput input)
        {
            if (input is JoyfulWorks.UniFoundationDev.Test.ISomeOtherInput typedInput)
            {
                Log.Output(LogCategory, $"{input.Name} registered as ISomeOtherInput");
                typedInput.FloatHappened += InvokeFloatHappened;
                typedInput.PositionChanged += InvokePositionChanged;
            }
        }

        private void UnregisterSomeOtherInput(IInput input)
        {
            if (input is JoyfulWorks.UniFoundationDev.Test.ISomeOtherInput typedInput)
            {
                Log.Output(LogCategory, $"{input.Name} unregistered as ISomeOtherInput");
                typedInput.FloatHappened -= InvokeFloatHappened;
                typedInput.PositionChanged -= InvokePositionChanged;
            }
        }

        #endregion
    }
}