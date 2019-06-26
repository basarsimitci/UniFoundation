using System.Globalization;
using UnityEngine;

namespace UniFoundation.Extensions.Colour
{
    public static class Color32Extensions
    {
        public static void FromHexString(this Color32 value, string hexString)
        {
            if ((hexString.StartsWith("#") == false) ||
                (((hexString.Length == 4) || (hexString.Length == 7)) == false))
            {
                return;
            }

            string redString = hexString.Length == 4
                ? hexString.Substring(1, 1)
                : hexString.Substring(1, 2);
            string greenString = hexString.Length == 4
                ? hexString.Substring(2, 1)
                : hexString.Substring(3, 2);
            string blueString = hexString.Length == 4
                ? hexString.Substring(3, 1)
                : hexString.Substring(5, 2);

            byte.TryParse(redString, NumberStyles.HexNumber, null, out value.r);
            byte.TryParse(greenString, NumberStyles.HexNumber, null, out value.g);
            byte.TryParse(blueString, NumberStyles.HexNumber, null, out value.b);
        }

        public static string ToHexString(this Color32 value)
        {
            string hexString = "#";
            hexString += value.r.ToString("x2");
            hexString += value.g.ToString("x2");
            hexString += value.b.ToString("x2");
            return hexString;
        }
    }
}