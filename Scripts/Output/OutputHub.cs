using System.Collections.Generic;
using System.Linq;

namespace UniFoundation.Output
{
    public abstract class OutputHub : IOutputHub
    {
        public const string LogCategory = "OutputHub";
        
        public string Name => "OutputHub";

        protected readonly List<IOutput> Outputs = new List<IOutput>();

        public virtual void RegisterOutput(IOutput output)
        {
            if (Outputs.Contains(output) == false)
            {
                Outputs.Add(output);
            }
        }

        public virtual void UnregisterOutput(IOutput output)
        {
            Outputs.Remove(output);
        }

        protected List<T> FindOutputsOfType<T>() where T : IOutput
        {
            return Outputs.FindAll(typeof(T).IsInstanceOfType).Cast<T>().ToList();
        }
    }
}
