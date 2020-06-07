using System;
using System.Drawing;

namespace DDSharp.S3TC
{
    /// <summary>
    /// Methods to handle colors in S3 compressions.
    /// </summary>
    public static class ColorMethods
    {
        /// <summary>
        /// Convert R5G6B5 binary representation to a Color value.
        /// </summary>
        public static Color Decode565(ushort raw)
        {
            // Red is the five most significant bits.
            ushort rbin = (ushort)((raw & 0xf800) >> 11);
            // Green is six bits, offset by five from the least significant bit.
            ushort gbin = (ushort)((raw & 0x07e0) >> 5);
            // Blue is the five least significant bits.
            ushort bbin = (ushort)(raw & 0x001f);

            float r = rbin / (float)31;
            float g = gbin / (float)63;
            float b = bbin / (float)31;

            return Color.FromArgb(255, (byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
        }

        /// <summary>
        /// Convert a color to R5G6B5 binary format.
        /// </summary>
        public static ushort Encode565(Color color)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Linear interpolation between two colors.
        /// </summary>
        public static Color Lerp(Color a, Color b, float t)
        {
            byte red = (byte)((t * a.R) + ((1 - t) * b.R));
            byte green = (byte)((t * a.G) + ((1 - t) * b.G));
            byte blue = (byte)((t * a.B) + ((1 - t) * b.B));
            byte alpha = (byte)((t * a.A) + ((1 - t) * b.A));
            return Color.FromArgb(alpha, red, green, blue);
        }
    }
}
