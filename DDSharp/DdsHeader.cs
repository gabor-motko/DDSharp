namespace DDSharp
{
    /// <summary>
    /// Standard DDS header.
    /// </summary>
    public class DdsHeader
    {
        // DWORD dwSize = 124;
        /// <summary>
        /// Flags that indicate which members contain valid data.
        /// </summary>
        public DdsHeaderFlags Flags { get; set; }
        /// <summary>
        /// The image height in pixels.
        /// </summary>
        public uint Height { get; set; }
        /// <summary>
        /// The image width in pixels.
        /// </summary>
        public uint Width { get; set; }
        /// <summary>
        /// The number of bytes in a scanline (if uncompressed), or the total number of bytes in the main surface (if compressed).
        /// To get the number of pixels in a scanline, use the formula Pixels = Pitch / (PixelFormat.RgbBitCount / 8).
        /// </summary>
        public uint Pitch { get; set; }
        /// <summary>
        /// Depth of a volume texture.
        /// </summary>
        public uint Depth { get; set; }
        /// <summary>
        /// The number of mipmap levels.
        /// </summary>
        public uint MipMapCount { get; set; }
        // DWORD dwReserved1[11];   // unused
        /// <summary>
        /// Details about the pixel format.
        /// </summary>
        public DdsPixelFormat PixelFormat { get; set; }
        /// <summary>
        /// Additional details about the texture.
        /// </summary>
        public DdsCaps1Flags Caps1 { get; set; }
        /// <summary>
        /// Additional details about the texture.
        /// </summary>
        public DdsCaps2Flags Caps2 { get; set; }
        // DWORD dwCaps3;   // Unused
        // DWORD dwCaps4;   // Unused
        // DWORD Reserved2;   // Unused

        public override string ToString()
        {
            return string.Format("DDS Header (Width: {0}, Height: {1}, Pitch: {3}, PixelFormat: {2})", Width, Height, PixelFormat.ToString(), Pitch);
        }

        public DdsHeader() { }

        public DdsHeader(uint width, uint height)
        {
            Width = width;
            Height = height;
        }
    }
}
