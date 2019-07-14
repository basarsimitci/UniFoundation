using System;
using UnityEngine;

namespace UniFoundation.Extensions.Colour
{
    public static class Color32Extensions
    {
        public static string ToHexString(this Color value)
        {
            string hexString = "#";
            hexString += Convert.ToByte(value.r).ToString("x2");
            hexString += Convert.ToByte(value.g).ToString("x2");
            hexString += Convert.ToByte(value.b).ToString("x2");
            return hexString;
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