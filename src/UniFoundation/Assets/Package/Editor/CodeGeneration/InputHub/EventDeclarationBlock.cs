using System;
using System.Linq;
using System.Reflection;

namespace JoyfulWorks.UniFoundation.Editor.CodeGeneration.InputHub
{
    public class EventDeclarationBlock
    {
        public readonly string Code;

        public const string Template = "        public event [EventHandlerType] [EventName];\n";

        public EventDeclarationBlock(EventInfo eventInfo)
        {
            if (eventInfo == null) throw new ArgumentNullException(nameof(eventInfo));

            string eventHandlerType = eventInfo.EventHandlerType.FullName?.Split('`').First();
            if (eventInfo.EventHandlerType.GenericTypeArguments.Length > 0)
            {
                eventHandlerType += "<";
                for (int argIndex = 0; argIndex < eventInfo.EventHandlerType.GenericTypeArguments.Length; argIndex++)
                {
                    if (argIndex > 0)
                    {
                        eventHandlerType += ", ";
                    }

                    Type type = eventInfo.EventHandlerType.GenericTypeArguments[argIndex];
                    eventHandlerType += type.FullName;
                }
                eventHandlerType += ">";
            }
            
            Code = Template
                .Replace("[EventHandlerType]", eventHandlerType)
                .Replace("[EventName]", eventInfo.Name);
        }
    }
}