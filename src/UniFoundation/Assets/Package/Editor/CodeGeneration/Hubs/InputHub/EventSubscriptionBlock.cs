namespace JoyfulWorks.UniFoundation.Editor.CodeGeneration.Hubs.InputHub
{
    public class EventSubscriptionBlock
    {
        public readonly string Code;

        private const string Template = "                typedInput.[EventName] [Operator]= Invoke[EventName];\n";

        public EventSubscriptionBlock(string eventName, bool subscribe = true)
        {
            string op = subscribe ? "+" : "-";
            Code = Template
                .Replace("[EventName]", eventName)
                .Replace("[Operator]", op);
        }
    }
}