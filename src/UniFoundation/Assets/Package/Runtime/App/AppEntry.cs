using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JoyfulWorks.UniFoundation.App
{
    public class AppEntry : MonoBehaviour, IAppLifetimeInput
    {
        private App app;

        public event Action AppLostFocus;
        public event Action AppGainedFocus;
        public event Action AppPaused;
        public event Action AppResumed;
        public event Action AppEnding;

        public string Name => gameObject.name;

        private void Awake()
        {
            //Find the type extending App in the default assembly.
            List<Type> appTypes = App.GetTypesInDefaultAssembly<App>().ToList();
            // If App is not extended, or if there more than 1 type extending App, fall back to base App.
            Type appType = appTypes.Count == 1 ? appTypes[0] : typeof(App);

            app = Activator.CreateInstance(appType) as App;
            
            DontDestroyOnLoad(this);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                AppGainedFocus?.Invoke();
            }
            else
            {
                AppLostFocus?.Invoke();
            }
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                AppPaused?.Invoke();
            }
            else
            {
                AppResumed?.Invoke();
            }
        }

        private void OnApplicationQuit()
        {
            AppEnding?.Invoke();
        }
    }
}