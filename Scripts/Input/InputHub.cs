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
            if (Inputs.Contains(input) == false)
            {
                Inputs.Add(input);
            }
        }

        public virtual void UnregisterInput(IInput input)
        {
            Inputs.Remove(input);
        }
    }
}
