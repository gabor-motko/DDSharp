namespace DDSharp
{

    /// <summary>
    /// Pixel format definition.
    /// </summary>
    public class DdsPixelFormat
    {
        // DWORD dwSize = 32;   // Size is always 32 bytes.
        public DdsPixelFormatFlags Flags { get; set; }
        public string FourCC { get; set; }
        public uint RgbBitCount { get; set; }
        public uint RedBitMask { get; set; }
        public uint GreenBitMask { get; set; }
        public uint BlueBitMask { get; set; }
        public uint AlphaBitMask { get; set; }

        public override string ToString()
        {
            if (Flags.HasFlag(DdsPixelFormatFlags.FourCC))
            {
                return string.Format("DDSpf (FourCC: {0})", FourCC);
            }
            else
            {
                return string.Format("DDSpf (BPP: {4}, R: {0:x8}, G: {1:x8}, B: {2:x8}, A: {3:x8})", RedBitMask, GreenBitMask, BlueBitMask, AlphaBitMask, RgbBitCount);
            }
        }
    }
}
