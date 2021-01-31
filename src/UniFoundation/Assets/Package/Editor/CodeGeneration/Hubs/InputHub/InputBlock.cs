using System;
using System.Reflection;

namespace JoyfulWorks.UniFoundation.Editor.CodeGeneration.Hubs.InputHub
{
    public class InputBlock
    {
        public readonly string Code;

        private const string Template =
            "        #region [InputType]\n" +
            "\n" +
            "[Events]" +
            "\n" +
            "[InvokeMethods]" +
            "        private void Register[InputName](IInput input)\n" +
            "        {\n" +
            "            if (input is [InputType] typedInput)\n" +
            "            {\n" +
            "                Log.Output(LogCategory, $\"{input.Name} registered as I[InputName]\");\n" +
            "[Subscriptions]" +
            "            }\n" +
            "        }\n" +
            "\n" +
            "        private void Unregister[InputName](IInput input)\n" +
            "        {\n" +
            "            if (input is [InputType] typedInput)\n" +
            "            {\n" +
            "                Log.Output(LogCategory, $\"{input.Name} unregistered as I[InputName]\");\n" +
            "[Unsubscriptions]" +
            "            }\n" +
            "        }\n" +
            "\n" +
            "        #endregion\n";

        public InputBlock(Type inputType)
        {
            string events = string.Empty;
            string invokeMethods = string.Empty;
            string subscriptions = string.Empty;
            string unsubscriptions = string.Empty;
            foreach (EventInfo eventInfo in inputType.GetEvents())
            {
                events += new EventDeclarationBlock(eventInfo).Code;
                invokeMethods += new EventInvokeBlock(eventInfo).Code + "\n";
                subscriptions += new EventSubscriptionBlock(eventInfo.Name).Code;
                unsubscriptions += new EventSubscriptionBlock(eventInfo.Name, false).Code;
            }

            Code = Template
                .Replace("[InputType]", $"{inputType.FullName}")
                .Replace("[InputName]", $"{inputType.Name.TrimStart('I')}")
                .Replace("[Events]", events)
                .Replace("[InvokeMethods]", invokeMethods)
                .Replace("[Subscriptions]", subscriptions)
                .Replace("[Unsubscriptions]", unsubscriptions);
        }
    }
}