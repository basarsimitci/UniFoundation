using JoyfulWorks.UniFoundation.Input;
using JoyfulWorks.UniFoundation.Logging;
using JoyfulWorks.UniFoundation.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JoyfulWorks.UniFoundation.App
{
    public class App
    {
        protected const string LogCategory = "App";

        internal ViewConfig ViewConfig;

        public static App Instance { get; private set; }
        
        public IInputHub InputHub { get; private set; }
        public IOutputHub OutputHub { get; private set; }
        public ISceneNavigator SceneNavigator { get; private set; }

        public App(IAppLifetimeInput appLifetimeInput, ViewConfig viewConfig)
        {
            Instance = this;
            ViewConfig = viewConfig;

            InitLogging();
            
            InitHubs();
            if (appLifetimeInput != null)
            {
                InputHub.RegisterInput(appLifetimeInput);
            }

            InitSceneNavigator();
        }

        public static IEnumerable<Type> GetTypesInDefaultAssembly<T>()
        {
            Assembly defaultAssembly = AppDomain.CurrentDomain.GetAssemblies().First(assembly => assembly.GetName().Name == "Assembly-CSharp");
            return defaultAssembly.GetTypes().Where(type => typeof(T).IsAssignableFrom(type));
        }

        public IEnumerable<Type> GetTypesInSameAssembly<T>()
        {
            return GetType().Assembly.GetTypes().Where(type => typeof(T).IsAssignableFrom(type));
        }

        public virtual void EndApp()
        {
        }

        public T GetInput<T>() where T : IInput
        {
            return (T) InputHub;
        }

        public T GetOutput<T>() where T : IOutput
        {
            return (T) OutputHub;
        }

        public T GetViewConfig<T>() where T : class
        {
            return ViewConfig as T;
        }

        private void InitLogging()
        {
            Log.SetStackTraceLevels();
            Log.RegisterLogOutput(new ConsoleLogOutput(), Log.AllCategories);
        }

        private void InitHubs()
        {
            Type inputHubType = GetTypesInSameAssembly<InputHub>().FirstOrDefault();
            if (inputHubType != null)
            {
                InputHub = Activator.CreateInstance(inputHubType) as IInputHub;
            }
            
            Type outputHubType = GetTypesInSameAssembly<OutputHub>().FirstOrDefault();
            if (outputHubType != null)
            {
                OutputHub = Activator.CreateInstance(outputHubType) as IOutputHub;
            }
        }

        private void InitSceneNavigator()
        {
            SceneNavigator = new SceneNavigator();
        }
    }
}