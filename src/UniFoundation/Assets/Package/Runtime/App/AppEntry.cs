using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JoyfulWorks.UniFoundation.App
{
    public class AppEntry : MonoBehaviour
    {
        private App app;

        private void Awake()
        {
            //Find the type extending App in the default assembly.
            List<Type> appTypes = App.GetTypesInDefaultAssembly<App>().ToList();
            // If App is not extended, or if there more than 1 type extending App, fall back to base App.
            Type appType = appTypes.Count == 1 ? appTypes[0] : typeof(App);

            app = Activator.CreateInstance(appType) as App;
            
            DontDestroyOnLoad(this);
        }
    }
}