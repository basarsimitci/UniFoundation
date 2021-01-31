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
        public static App Instance { get; private set; }
        
        public IInputHub InputHub { get; private set; }
        public IOutputHub OutputHub { get; private set; }

        public App()
        {
            Instance = this;

            InitLogging();
            InitHubs();
        }

        public static IEnumerable<Type> GetTypesInDefaultAssembly<T>()
        {
            Assembly defaultAssembly = AppDomain.CurrentDomain.GetAssemblies().First(assembly => assembly.GetName().Name == "Assembly-CSharp");
            return defaultAssembly.GetTypes().Where(type => typeof(T).IsAssignableFrom(type));
        }

        public T GetInput<T>() where T : IInput
        {
            return (T) InputHub;
        }

        public T GetOutput<T>() where T : IOutput
        {
            return (T) OutputHub;
        }
        
        private void InitLogging()
        {
            Log.SetStackTraceLevels();
            Log.RegisterLogOutput(new ConsoleLogOutput(), Log.AllCategories);
        }

        private void InitHubs()
        {
            Type inputHubType = GetTypesInDefaultAssembly<InputHub>().FirstOrDefault();
            if (inputHubType != null)
            {
                InputHub = Activator.CreateInstance(inputHubType) as IInputHub;
            }
            
            Type outputHubType = GetTypesInDefaultAssembly<OutputHub>().FirstOrDefault();
            if (outputHubType != null)
            {
                OutputHub = Activator.CreateInstance(outputHubType) as IOutputHub;
            }
        }
   }
}