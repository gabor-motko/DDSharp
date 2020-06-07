using System;
using System.Collections.Generic;
using System.Drawing;

namespace DDSharp.S3TC
{
    public static class Dxt1
    {
        /// <summary>
        /// Decode a 4x4 DXT1/BC1 texel block.
        /// </summary>
        public static Color[,] DecodePixel(byte[] pixel)
        {
            ushort color0Binary = BitConverter.ToUInt16(pixel, 0);
            ushort color1Binary = BitConverter.ToUInt16(pixel, 2);

            bool hasAlpha = color0Binary <= color1Binary;   // If true, the image has a one-bit alpha channel.

            byte[] indexTableBytes = new byte[4];   // 16 two-byte indices.
            Buffer.BlockCopy(pixel, 4, indexTableBytes, 0, 4);

            // Reference colors are stored as R5G6B5.
            Color color0 = ColorMethods.Decode565(color0Binary);
            Color color1 = ColorMethods.Decode565(color0Binary);
            Color color2 = hasAlpha ? ColorMethods.Lerp(color0, color1, 0.5f) : ColorMethods.Lerp(color0, color1, 2f / 3f);
            Color color3 = hasAlpha ? Color.FromArgb(0, 0, 0, 0) : ColorMethods.Lerp(color0, color1, 1f / 3f);  // If the image has alpha, an index of 3 represents a transparent pixel.

            // Put all ref colors in an array for easier indexing.
            Color[] refColors = new Color[] { color0, color1, color2, color3 };

            // Assign the texel colors.
            Color[,] texels = new Color[4,4];
            for (int i = 0; i < 4; ++i)
            {
                texels[i, 0] = refColors[(indexTableBytes[i] & 0xc0) >> 6];
                texels[i, 1] = refColors[(indexTableBytes[i] & 0x30) >> 4];
                texels[i, 2] = refColors[(indexTableBytes[i] & 0x0c) >> 2];
                texels[i, 3] = refColors[indexTableBytes[i] & 0x03];
            }

            return texels;
        }

        /// <summary>
        /// Compress a 4x4 texel block using DXT1/BC1 compression.
        /// </summary>
        public static byte[] EncodePixel(Color[,] block)
        {
            // Calculate the two reference colors... somehow.
            // Calculate the other two reference colors.
            // Figure out which color the 16 pixels are closest to.
            // Encode the values.
            throw new NotImplementedException();
        }
    }
}
