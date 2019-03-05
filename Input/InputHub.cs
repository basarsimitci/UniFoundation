using System.Collections.Generic;

namespace UniFoundation.Input
{
    public abstract class InputHub : IInputHub
    {
        protected List<IInput> Inputs = new List<IInput>();

        public virtual void RegisterInput(IInput input)
        {
            Inputs.Add(input);
        }
    }
}
