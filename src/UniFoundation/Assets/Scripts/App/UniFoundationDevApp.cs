using JoyfulWorks.UniFoundation.App;

namespace JoyfulWorks.UniFoundationDev.App
{
    public class UniFoundationDevApp : UniFoundation.App.App
    {
        public new static UniFoundationDevApp Instance => (UniFoundationDevApp) UniFoundation.App.App.Instance;

        // Declare properties for your own services here.
        // For example;
        //public ISomeService SomeService { get; }

        public UniFoundationDevApp(IAppLifetimeInput appLifetimeInput, ViewConfig viewConfig) : base(appLifetimeInput, viewConfig)
        {
            // Instantiate your own services here.
            // SomeService = new ConcreteSomeService();
        }
    }
}