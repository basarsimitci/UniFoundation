namespace JoyfulWorks.UniFoundation.Extensions.Types
{
    public static class BoolExtensions
    {
        public static int GetBytes(this bool value, byte[] buffer, int offset)
        {
            if (buffer.Length >= (offset + sizeof(int)))
            {
                unsafe
                {
                    fixed (byte* bufferPtr = &buffer[offset])
                    {
                        *(bool*)bufferPtr = value;
                    }
                }
                return sizeof(bool);
            }

            return 0;
        }
        
    }
}