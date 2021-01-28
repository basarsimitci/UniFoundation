namespace JoyfulWorks.UniFoundation.Output
{
    public interface IOutputHub
    {
        void RegisterOutput(IOutput output);
        void UnregisterOutput(IOutput output);
    }
}