using System.Collections.Generic;

namespace UniFoundation.Output
{
    public abstract class OutputHub : IOutputHub
    {
        public const string LogCategory = "OutputHub";
        
        public string Name => "OutputHub";

        protected List<IOutput> Outputs = new List<IOutput>();

        public virtual void RegisterOutput(IOutput output)
        {
            Outputs.Add(output);
        }
    }
}
