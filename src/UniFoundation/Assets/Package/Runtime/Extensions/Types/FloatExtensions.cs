namespace JoyfulWorks.UniFoundation.Extensions.Types
{
    public static class FloatExtensions
    {
        public static int GetBytes(this float value, byte[] buffer, int offset)
        {
            if (buffer.Length >= (offset + sizeof(float)))
            {
                unsafe
                {
                    fixed (byte* bufferPtr = &buffer[offset])
                    {
                        *(float*)bufferPtr = value;
                    }
                }
                return sizeof(float);
            }

            return 0;
        }
    }
}