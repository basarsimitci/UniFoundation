using System.Collections.Generic;

namespace UniFoundation.Input
{
    public abstract class InputHub : IInputHub
    {
        public const string LogCategory = "InputHub";
        
        public string Name => "InputHub";

        protected readonly List<IInput> Inputs = new List<IInput>();

        public virtual void RegisterInput(IInput input)
        {
            Inputs.Add(input);
        }
    }
}
