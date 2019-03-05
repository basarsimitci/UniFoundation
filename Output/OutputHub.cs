using System.Collections.Generic;

namespace UniFoundation.Output
{
    public abstract class OutputHub : IOutputHub
    {
        protected List<IOutput> Outputs = new List<IOutput>();

        public virtual void RegisterOutput(IOutput output)
        {
            Outputs.Add(output);
        }
    }
}
