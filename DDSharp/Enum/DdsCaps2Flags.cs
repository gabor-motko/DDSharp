using System;

namespace DDSharp
{
    [Flags]
    public enum DdsCaps2Flags
    {
        /// <summary>
        /// No additional details apply to the image.
        /// </summary>
        None = 0,
        /// <summary>
        /// Indicates that the image is a cubemap.
        /// </summary>
        Cubemap = 0x200,
        /// <summary>
        /// Indicates that the image contains the X+ face of the cubemap.
        /// </summary>
        PositiveX = 0x400,
        /// <summary>
        /// Indicates that the image contains the X- face of the cubemap.
        /// </summary>
        NegativeX = 0x800,
        /// <summary>
        /// Indicates that the image contains the Y+ face of the cubemap.
        /// </summary>
        PositiveY = 0x1000,
        /// <summary>
        /// Indicates that the image contains the Y- face of the cubemap.
        /// </summary>
        NegativeY = 0x2000,
        /// <summary>
        /// Indicates that the image contains the Z+ face of the cubemap.
        /// </summary>
        PositiveZ = 0x4000,
        /// <summary>
        /// Indicates that the image contains the Z- face of the cubemap.
        /// </summary>
        NegativeZ = 0x8000,
        /// <summary>
        /// Indicates that the image is a volume texture.
        /// </summary>
        Volume = 0x200000,
        /// <summary>
        /// Indicates a cubemap with all six faces present. Required for Direct3D 10 and later.
        /// </summary>
        CubemapAllFaces = 0xfe00
    }
}
