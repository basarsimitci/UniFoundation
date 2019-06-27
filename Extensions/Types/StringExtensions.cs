using System.Globalization;
using UnityEngine;

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
        
        public static Color32 ToColor32(this string value)
        {
            Color32 colour = Color.clear;

            if ((value.StartsWith("#") == false) ||
                (((value.Length == 4) || (value.Length == 7)) == false))
            {
                return colour;
            }

            string redString = value.Length == 4
                ? value.Substring(1, 1)
                : value.Substring(1, 2);
            string greenString = value.Length == 4
                ? value.Substring(2, 1)
                : value.Substring(3, 2);
            string blueString = value.Length == 4
                ? value.Substring(3, 1)
                : value.Substring(5, 2);

            byte.TryParse(redString, NumberStyles.HexNumber, null, out colour.r);
            byte.TryParse(greenString, NumberStyles.HexNumber, null, out colour.g);
            byte.TryParse(blueString, NumberStyles.HexNumber, null, out colour.b);
            
            return colour;
        }
    }
}