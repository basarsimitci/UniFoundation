namespace UniFoundation.Input
{
    public interface IInputHub
    {
        void RegisterInput(IInput input);
        void UnregisterInput(IInput input);
    }
}