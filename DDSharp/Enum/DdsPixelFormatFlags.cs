using System;

namespace DDSharp
{
    [Flags]
    public enum DdsPixelFormatFlags
    {
        /// <summary>
        /// The pixel format has alpha channel information.
        /// </summary>
        AlphaPixels = 0x1,
        /// <summary>
        /// The pixel format contains only monochromatic alpha channel information.
        /// </summary>
        AlphaOnly = 0x2,
        /// <summary>
        /// The image data is compressed (S3TC/DXTn) or the image data is a DXGI pixel format.
        /// </summary>
        FourCC = 0x4,
        /// <summary>
        /// The image data is uncompressed.
        /// </summary>
        UncompressedRGB = 0x40,
        /// <summary>
        /// The image contains uncompressed YUV data. Y/U/V channels map to R/G/B bit masks respectively.
        /// </summary>
        Yuv = 0x200,
        /// <summary>
        /// The image contains uncompressed image data on a single channel. Luminance maps to the red bit mask.
        /// </summary>
        Luminance = 0x20000
    }
}
