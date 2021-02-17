using JoyfulWorks.UniFoundation.Input;
using JoyfulWorks.UniFoundation.Logging;

namespace JoyfulWorks.UniFoundationDev.App
{
    public class UniFoundationDevInputHub : InputHub,
        JoyfulWorks.UniFoundationDev.Test.ISomeInput,
        JoyfulWorks.UniFoundationDev.Test.ISomeOtherInput,
        JoyfulWorks.UniFoundation.App.IAppLifetimeInput
    {
        public override void RegisterInput(IInput input)
        {
            if (Inputs.Contains(input) == false)
            {
                base.RegisterInput(input);
                RegisterSomeInput(input);
                RegisterSomeOtherInput(input);
                RegisterAppLifetimeInput(input);
            }
        }

        public override void UnregisterInput(IInput input)
        {
            if (Inputs.Contains(input))
            {
                base.UnregisterInput(input);
                UnregisterSomeInput(input);
                UnregisterSomeOtherInput(input);
                UnregisterAppLifetimeInput(input);
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

        #region JoyfulWorks.UniFoundation.App.IAppLifetimeInput

        public event System.Action AppLostFocus;
        public event System.Action AppGainedFocus;
        public event System.Action AppPaused;
        public event System.Action AppResumed;
        public event System.Action AppEnding;

        private void InvokeAppLostFocus()
        {
            AppLostFocus?.Invoke();
        }

        private void InvokeAppGainedFocus()
        {
            AppGainedFocus?.Invoke();
        }

        private void InvokeAppPaused()
        {
            AppPaused?.Invoke();
        }

        private void InvokeAppResumed()
        {
            AppResumed?.Invoke();
        }

        private void InvokeAppEnding()
        {
            AppEnding?.Invoke();
        }

        private void RegisterAppLifetimeInput(IInput input)
        {
            if (input is JoyfulWorks.UniFoundation.App.IAppLifetimeInput typedInput)
            {
                Log.Output(LogCategory, $"{input.Name} registered as IAppLifetimeInput");
                typedInput.AppLostFocus += InvokeAppLostFocus;
                typedInput.AppGainedFocus += InvokeAppGainedFocus;
                typedInput.AppPaused += InvokeAppPaused;
                typedInput.AppResumed += InvokeAppResumed;
                typedInput.AppEnding += InvokeAppEnding;
            }
        }

        private void UnregisterAppLifetimeInput(IInput input)
        {
            if (input is JoyfulWorks.UniFoundation.App.IAppLifetimeInput typedInput)
            {
                Log.Output(LogCategory, $"{input.Name} unregistered as IAppLifetimeInput");
                typedInput.AppLostFocus -= InvokeAppLostFocus;
                typedInput.AppGainedFocus -= InvokeAppGainedFocus;
                typedInput.AppPaused -= InvokeAppPaused;
                typedInput.AppResumed -= InvokeAppResumed;
                typedInput.AppEnding -= InvokeAppEnding;
            }
        }

        #endregion
    }
}