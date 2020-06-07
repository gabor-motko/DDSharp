using System;

namespace DDSharp
{
    /// <summary>
    /// Indicates which members contain valid data.
    /// </summary>
    [Flags]
    public enum DdsHeaderFlags
    {
        /// <summary>
        /// Always required.
        /// </summary>
        Caps = 0x1,

        /// <summary>
        /// Always required.
        /// </summary>
        Height = 0x2,

        /// <summary>
        /// Always required.
        /// </summary>
        Width = 0x4,

        /// <summary>
        /// Pitch, if the texture is uncompressed.
        /// </summary>
        Pitch = 0x8,

        /// <summary>
        /// Always required.
        /// </summary>
        PixelFormat = 0x1000,

        /// <summary>
        /// Required in a mipmapped texture.
        /// </summary>
        MipMapCount = 0x20000,

        /// <summary>
        /// Pitch, if the texture is compressed.
        /// </summary>
        LinearSize = 0x80000,

        /// <summary>
        /// Required in a depth texture.
        /// </summary>
        Depth = 0x800000
    }
}
