using System;

namespace DDSharp
{
    [Flags]
    public enum DdsCaps1Flags
    {
        /// <summary>
        /// Indicates that the image contains multiple surfaces.
        /// </summary>
        Complex = 0x8,

        /// <summary>
        /// Indicates that the image contains mipmaps.
        /// </summary>
        MipMap = 0x400000,

        /// <summary>
        /// Always required.
        /// </summary>
        Texture = 0x1000
    }
}
