using JoyfulWorks.UniFoundation.Extensions.Types;
using JoyfulWorks.UniFoundation.Logging;
using System;

namespace JoyfulWorks.UniFoundation.Networking
{
    public abstract class NetworkMessage
    {
        public int Id { get; protected set; }
        
        public virtual int Size => sizeof(int);

        public virtual int Serialize(byte[] buffer)
        {
            int numberOfBytes = 0;

            if (buffer.Length >= Size)
            {
                numberOfBytes += Id.GetBytes(buffer, numberOfBytes);
            }
            else
            {
                Log.Output(Network.LogCategory,
                    $"Could not serialize {GetType()}. Destination buffer is too small.",
                    LogLevel.Error);
            }

            return numberOfBytes;
        }

        public virtual int Deserialize(byte[] buffer)
        {
            int numberOfBytes = 0;
            
            Id = BitConverter.ToInt32(buffer, numberOfBytes);
            numberOfBytes += sizeof(int);

            return numberOfBytes;
        }
    }
}
