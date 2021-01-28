namespace JoyfulWorks.UniFoundation.Extensions.Types
{
    public static class IntExtensions
    {
        public static int GetBytes(this int value, byte[] buffer, int offset)
        {
            if (buffer.Length >= (offset + sizeof(int)))
            {
                unsafe
                {
                    fixed (byte* bufferPtr = &buffer[offset])
                    {
                        *(int*)bufferPtr = value;
                    }
                }
                return sizeof(int);
            }

            return 0;
        }
    }
}