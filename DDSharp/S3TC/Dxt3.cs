using System;
using System.Collections.Generic;
using System.Drawing;

namespace DDSharp.S3TC
{
    public static class Dxt3
    {
        public static int GetPixelAlpha(byte[] buffer, int index)
        {
            if(index % 2 == 0)
            {
                // If the index is even, the alpha value is in the first four bits.
                int a = buffer[index / 2] & 0x0f;
                return (int)((a / 15f) * 255);
            }
            else
            {
                // If the index is odd, the alpha value is in the second four bits.
                int a = (buffer[index / 2] & 0xf0) >> 4;
                return (int)((a / 15f) * 255);
            }

        }

        /// <summary>
        /// Decode a 4x4 DXT3/BC2 texel block.
        /// </summary>
        public static Color[,] DecodePixel(byte[] pixel)
        {
            // The first 8 bytes contain alpha data
            byte[] alphaData = new byte[8];
            Buffer.BlockCopy(pixel, 0, alphaData, 0, 8);

            // The second 8 bytes contain color data
            byte[] colorData = new byte[8];
            Buffer.BlockCopy(pixel, 8, colorData, 0, 8);
            ushort color0Binary = BitConverter.ToUInt16(colorData, 0);
            ushort color1Binary = BitConverter.ToUInt16(colorData, 2);

            byte[] indexTableBytes = new byte[4];   // 16 two-byte indices.
            Buffer.BlockCopy(colorData, 4, indexTableBytes, 0, 4);

            // Reference colors are stored as R5G6B5.
            Color color0 = DxtCommon.Decode565(color0Binary);
            Color color1 = DxtCommon.Decode565(color0Binary);
            Color color2 = DxtCommon.Lerp(color0, color1, 2f / 3f);
            Color color3 = DxtCommon.Lerp(color0, color1, 1f / 3f);

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
