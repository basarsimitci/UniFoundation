using JoyfulWorks.UniFoundation.Logging;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace JoyfulWorks.UniFoundation.Networking
{
    public class NetworkNode
    {
        public event Action<string, NetworkMessage> ReceivedMessage;

        private readonly Socket transmissionSocket;
        private readonly byte[] transmissionBuffer = new byte[1024];
        private readonly IPEndPoint remoteEndpoint;

        private readonly Socket receptionSocket;
        private readonly byte[] receptionBuffer = new byte[1024];
        private readonly IPEndPoint localEndpoint;

        private IPEndPoint senderIpEp;
        
        private readonly INetworkMessageRegistry messageRegistry;

        public NetworkNode(IPEndPoint remoteEndpoint, IPEndPoint localEndpoint, INetworkMessageRegistry messageRegistry)
        {
            this.remoteEndpoint = remoteEndpoint;
            this.localEndpoint = localEndpoint;
            this.messageRegistry = messageRegistry;

            transmissionSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            receptionSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            try
            {
                if (remoteEndpoint != null)
                {
                    if (remoteEndpoint.Address.Equals(IPAddress.Broadcast))
                    {
                        transmissionSocket.EnableBroadcast = true;
                    }
                }

                if (localEndpoint != null)
                {
                    if (localEndpoint.Address.Equals(IPAddress.Broadcast) ||
                        localEndpoint.Address.Equals(IPAddress.Any))
                    {
                        receptionSocket.EnableBroadcast = true;
                    }

                    receptionSocket.Bind(localEndpoint);
                }
            }
            catch (Exception e)
            {
                Log.Output(Network.LogCategory, e.Message, LogLevel.Error);
            }
            
            senderIpEp = new IPEndPoint(IPAddress.Any, localEndpoint?.Port ?? 0);
        }

        public void Send(NetworkMessage message, IPEndPoint remoteEp = null)
        {
            if (transmissionSocket != null)
            {
                message.Serialize(transmissionBuffer);
                try
                {
                    transmissionSocket.SendTo(transmissionBuffer, message.Size,
                        SocketFlags.None,
                        remoteEp ?? remoteEndpoint);
                }
                catch (Exception e)
                {
                    Log.Output(Network.LogCategory, e.Message, LogLevel.Error);
                }
            }
        }
        
        public void StartReception()
        {
            if (receptionSocket != null)
            {
                Task.Run(ReceiveMessages);
            }
        }

        public void StopReception()
        {
        }

        private void ReceiveMessages()
        {
            while (true)
            {
                if (receptionSocket.Available > 0)
                {
                    while (receptionSocket.Available > 0)
                    {
                        try
                        {
                            EndPoint senderEp = senderIpEp;
                            receptionSocket.ReceiveFrom(receptionBuffer, ref senderEp);
                            NetworkMessage networkMessage = GetReceivedMessage();
                            if (networkMessage != null)
                            {
                                ReceivedMessage?.Invoke(((IPEndPoint)senderEp).Address.ToString(), networkMessage);
                            }
                        }
                        catch (Exception e)
                        {
                            Log.Output(Network.LogCategory, e.Message, LogLevel.Error);
                        }
                    }
                }
            }
        }
        
        private NetworkMessage GetReceivedMessage()
        {
            int messageId = BitConverter.ToInt32(receptionBuffer, 0);
            Type messageType = messageRegistry.GetMessageType(messageId);
            if (messageType != null)
            {
                if (Activator.CreateInstance(messageType) is NetworkMessage message)
                {
                    message.Deserialize(receptionBuffer);
                    return message;
                }
            }

            return null;
        }
    }
}