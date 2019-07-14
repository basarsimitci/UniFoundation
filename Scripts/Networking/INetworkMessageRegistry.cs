using System;

namespace UniFoundation.Networking
{
    public interface INetworkMessageRegistry
    {
        void RegisterMessageId<T>(int messageId) where T : NetworkMessage;
        Type GetMessageType(int messageId);
    }
}
