using System;
using System.Collections.Generic;
using UniFoundation.Logging;

namespace UniFoundation.Networking
{
    public class Network : INetworkMessageRegistry
    {
        public const string LogCategory = "Network";

        private readonly Dictionary<int, Type> messageIdToType = new Dictionary<int, Type>();
        
        public void RegisterMessageId<T>(int messageId) where T : NetworkMessage
        {
            if (messageIdToType.ContainsKey(messageId))
            {
                Log.Output(LogCategory, 
                    $"Message id {messageId} already registered for {messageIdToType[messageId]}",
                    LogLevel.Error);
                return;
            }
            messageIdToType[messageId] = typeof(T);
        }

        public Type GetMessageType(int messageId)
        {
            return messageIdToType.ContainsKey(messageId) ? messageIdToType[messageId] : null;
        }
    }
}