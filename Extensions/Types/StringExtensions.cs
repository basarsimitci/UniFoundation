namespace UniFoundation.Extensions.Types
{
    public static class StringExtensions
    {
        public static int GetBytes(this string value, byte[] buffer, int offset)
        {
            int numberOfBytes = sizeof(char) * value.Length;

            if (buffer.Length >= (offset + numberOfBytes))
            {
                unsafe
                {
                    for (int charIndex = 0; charIndex < value.Length; charIndex++)
                    {
                        fixed (byte* bufferPtr = &buffer[offset + (charIndex * sizeof(char))])
                        {
                            *(char*)bufferPtr = value[charIndex];
                        }
                    }
                }
                
                return numberOfBytes;
            }

            return 0;
        }
    }
}