using System;
using System.Linq;
using System.Reflection;

namespace JoyfulWorks.UniFoundation.Editor.CodeGeneration.Hubs.InputHub
{
    public class EventDeclarationBlock
    {
        public readonly string Code;

        public const string Template = "        public event [EventHandlerType] [EventName];\n";

        public EventDeclarationBlock(EventInfo eventInfo)
        {
            if (eventInfo == null) throw new ArgumentNullException(nameof(eventInfo));

            string eventHandlerType = CodeGenerator.GetTypeFullName(eventInfo.EventHandlerType);
            
            Code = Template
                .Replace("[EventHandlerType]", eventHandlerType)
                .Replace("[EventName]", eventInfo.Name);
        }
    }
}