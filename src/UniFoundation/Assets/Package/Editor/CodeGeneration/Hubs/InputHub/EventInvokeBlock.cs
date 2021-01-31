using System;
using System.Reflection;

namespace JoyfulWorks.UniFoundation.Editor.CodeGeneration.Hubs.InputHub
{
    public class EventInvokeBlock
    {
        public readonly string Code;

        private const string Template =
            "        private void Invoke[EventName]([EventParameters])\n" +
            "        {\n" +
            "            [EventName]?.Invoke([EventArguments]);\n" +
            "        }\n";

        public EventInvokeBlock(EventInfo eventInfo)
        {
            if (eventInfo == null) throw new ArgumentNullException(nameof(eventInfo));
            
            string eventName = eventInfo.Name;

            string eventParameters = string.Empty;
            string eventArguments = string.Empty;
            Type eventHandlerType = eventInfo.EventHandlerType;
            for (int argNo = 1; argNo <= eventHandlerType.GenericTypeArguments.Length; argNo++)
            {
                Type argumentType = eventHandlerType.GenericTypeArguments[argNo - 1];

                if (argNo > 1)
                {
                    eventParameters += ", ";
                    eventArguments += ", ";
                }
                
                eventParameters += $"{argumentType.FullName} arg{argNo}";
                eventArguments += $"arg{argNo}";
            }

            Code = Template
                .Replace("[EventName]", eventName)
                .Replace("[EventParameters]", eventParameters)
                .Replace("[EventArguments]", eventArguments);
        }
    }
}