﻿using System;
using System.IO;
using System.Threading.Tasks;
using System.Drawing;

namespace DDSharp
{
    /// <summary>
    /// Represents a DirectDraw Surface image.
    /// </summary>
    public class DDS
    {
        /// <summary>
        /// Standard DDS header.
        /// </summary>
        public DdsHeader Header { get; set; }

        /// <summary>
        /// Extended header. Valid only if Header.PixelFormat.FourCC == FourCC.DX10, otherwise null and ignored.
        /// </summary>
        public DdsHeaderDXT10 HeaderDXT10 { get; set; }

        /// <summary>
        /// Pixel color data of the main surface.
        /// </summary>
        public Color[] MainSurfacePixels { get; set; }

        /// <summary>
        /// Creates a bitmap from the pixel data.
        /// </summary>
        public Bitmap GetBitmap()
        {
            Bitmap image = new Bitmap((int)Header.Width, (int)Header.Height);
            for (int i = 0; i < MainSurfacePixels.Length; ++i)
            {
                image.SetPixel(i % (int)Header.Width, i / (int)Header.Width, MainSurfacePixels[i]);
            }
            return image;
        }

        public override string ToString()
        {
            return string.Format("DDS Image\n{0}", Header.ToString());
        }

        /// <summary>
        /// Create a new empty DDS image.
        /// </summary>
        public DDS() { }

        /// <summary>
        /// Create a new DDS image of the given size and initialize its header and main surface members.
        /// </summary>
        public DDS(uint width, uint height)
        {
            Header = new DdsHeader(width, height);
        }

        /// <summary>
        /// Get a masked channel value from the pixel data. The result is probably, but not necessarily, limited to eight bits.
        /// </summary>
        public static uint GetChannelFromPixel(uint data, uint bitmask, out uint maxValue)
        {
            // If the bitmask is zero, the channel is not used.
            if (bitmask == 0)
            {
                maxValue = 0;
                return 0;
            }
            // Filter out the relevant bits
            uint masked = data & bitmask;
            // Right-shift until the mask's least significant bit is set
            while ((bitmask & 1) == 0)
            {
                masked >>= 1;
                bitmask >>= 1;
            }
            maxValue = bitmask;
            return masked;
        }

        /// <summary>
        /// Loads a DDS image from the given file path.
        /// </summary>
        public static DDS LoadFromFile(string path, Action<string> log)
        {
            return LoadFromFile(File.Open(path, FileMode.Open), log);
        }

        /// <summary>
        /// Loads a DDS image from the given input stream.
        /// </summary>
        public static DDS LoadFromFile(Stream stream, Action<string> log)
        {
            DDS image = new DDS();
            using (BinaryReader reader = new BinaryReader(stream))
            {
                #region Header
                // A valid DDS file must contain a 4-byte signature followed by a 124-byte header.
                // Validate the stream length. If the file is shorter than 128 bytes, it can't possibly be valid.
                if (reader.BaseStream.Length < 128)
                {
                    throw new IOException("The DDS file is invalid because it is shorter than 128 bytes.");
                }
                // Read and validate the signature. If it's not "DDS " then the file is invalid.
                uint signature;
                if ((signature = reader.ReadUInt32()) != 0x20534444)
                {
                    throw new IOException(string.Format("The DDS file's signature is invalid. Expected 0x20534444, got 0x{0:x}.", signature));
                }

                // Begin reading the standard header.
                uint ddsHeaderLength = reader.ReadUInt32(); // The header length is always 124 bytes and thus may be ignored.

                DdsHeader header = image.Header = new DdsHeader();
                header.Flags = (DdsHeaderFlags)reader.ReadUInt32();    // DWORD containing the header flags.
                header.Height = reader.ReadUInt32();
                header.Width = reader.ReadUInt32();
                header.Pitch = reader.ReadUInt32();
                header.Depth = reader.ReadUInt32();
                header.MipMapCount = reader.ReadUInt32();
                reader.BaseStream.Seek(44, SeekOrigin.Current); // dwReserved1[11] 44 bytes of unused space

                // Pixel format
                uint pixelFormatLength = reader.ReadUInt32();   // Pixel format length is always 32 bytes and thus can be ignored.
                DdsPixelFormat pf = header.PixelFormat = new DdsPixelFormat();
                pf.Flags = (DdsPixelFormatFlags)reader.ReadUInt32();    // DWORD containing the pixel format flags.
                pf.FourCC = System.Text.Encoding.ASCII.GetString(reader.ReadBytes(4));    // 4 bytes representing a FourCC ASCII string
                pf.RgbBitCount = reader.ReadUInt32();
                pf.RedBitMask = reader.ReadUInt32();
                pf.GreenBitMask = reader.ReadUInt32();
                pf.BlueBitMask = reader.ReadUInt32();
                pf.AlphaBitMask = reader.ReadUInt32();

                header.Caps1 = (DdsCaps1Flags)reader.ReadUInt32();
                header.Caps2 = (DdsCaps2Flags)reader.ReadUInt32();
                reader.BaseStream.Seek(12, SeekOrigin.Current); // dwCaps3, dwCaps4, dwReserved2, 12 bytes of unused space

                // If the DdsPixelFormatFlags.FourCC flag is set and FourCC is DX10, then the file also has an extended DXT10 header. This is currently not supported.
                if (pf.Flags.HasFlag(DdsPixelFormatFlags.FourCC) && pf.FourCC == "DX10")
                {
                    throw new NotImplementedException("The DDS file has a DXT10 header, which is not supported.");
                }
                #endregion

                #region Main Surface
                // The main surface data begins here.
                // If the DdsPixelFormatFlags.UncompressedRGB flag is set, the image data is uncompressed and its length is Height * Width * BytesPerPixel.
                // If the DdsPixelFormatFlags.FourCC flag is set, the image data is either compressed (DXTn) or is in a DXGI pixel format. Length is equal to DdsHeader.Pitch.
                if (pf.Flags.HasFlag(DdsPixelFormatFlags.FourCC))
                {
                    switch (pf.FourCC)
                    {
                        case "DXT1":
                            {

                            }
                            break;
                        case "DXT3":
                            {

                            }
                            break;
                        case "DXT5":
                            {

                            }
                            break;
                        default:
                            throw new InvalidOperationException(string.Format("The FourCC identifier {0} is not valid.", pf.FourCC));
                    }
                }
                else if (pf.Flags.HasFlag(DdsPixelFormatFlags.UncompressedRGB))
                {
                    // Read and decode uncompressed RGB image data.
                    uint pixelSize = pf.RgbBitCount / 8;
                    uint length = header.Width * header.Height * pixelSize;
                    uint pixelCount = length / pixelSize;

                    image.MainSurfacePixels = new Color[pixelCount];

                    System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
                    stopwatch.Start();
                    for (int i = 0; i < pixelCount; ++i)
                    {
                        // The length of pixels is determined by DdsPixelFormat.RgbBitCount, but because bitmasks are DWORDs, they cannot exceed 32 bits.
                        byte[] bytes;
                        if (pf.RgbBitCount == 32)   // This test has a surprisingly positive effect on performance.
                        {
                            bytes = reader.ReadBytes((int)pixelSize);
                        }
                        else
                        {
                            bytes = new byte[4];
                            Buffer.BlockCopy(reader.ReadBytes((int)pixelSize), 0, bytes, 0, (int)pixelSize);
                        }
                        uint pixelData = BitConverter.ToUInt32(bytes, 0);

                        // The raw channel data is not necessarily equivalent to the color value.
                        uint redChannel = GetChannelFromPixel(pixelData, pf.RedBitMask, out uint redMax);
                        uint greenChannel = GetChannelFromPixel(pixelData, pf.GreenBitMask, out uint greenMax);
                        uint blueChannel = GetChannelFromPixel(pixelData, pf.BlueBitMask, out uint blueMax);
                        uint alphaChannel = GetChannelFromPixel(pixelData, pf.AlphaBitMask, out uint alphaMax); // Alpha channel may not be present, in which case alphaChannel and alphaMax are 0 and pixel alpha is later set to 1.

                        // The actual color is the channel value divided by the maximum value (which is the rightmost-shifted bitmask).
                        float r = (float)redChannel / redMax;
                        float g = (float)greenChannel / greenMax;
                        float b = (float)blueChannel / blueMax;
                        float a = pf.Flags.HasFlag(DdsPixelFormatFlags.AlphaPixels) ? (float)alphaChannel / alphaMax : 1;   // If the image does not contain alpha information, assume alpha is 1.

                        image.MainSurfacePixels[i] = Color.FromArgb((int)(a * 255), (int)(r * 255), (int)(g * 255), (int)(b * 255));
                    }
                    stopwatch.Stop();
                    log(stopwatch.ElapsedMilliseconds.ToString());
                }
                else if (pf.Flags.HasFlag(DdsPixelFormatFlags.Yuv))
                {
                    // Decode uncompressed YUV data, TBI
                    throw new InvalidOperationException("YUV pixel format is currently not supported.");
                }
                else if (pf.Flags.HasFlag(DdsPixelFormatFlags.Luminance))
                {
                    // Decode uncompressed single channel luminance data
                    throw new InvalidOperationException("Luminance pixel format is currently not supported.");
                }
                else
                {
                    throw new InvalidOperationException("Invalid PixelFormat, either the 0x4 or 0x40 flag has to be set.");
                }
                #endregion

                #region Other surfaces
                // The rest of the file contains additional surfaces. Their count and order are defined by the DdsHeader.Caps1 and DdsHeader.Caps2 flags.
                // For mipmaps, the count is defined by a formula, see https://docs.microsoft.com/en-us/windows/win32/direct3ddds/dds-file-layout-for-textures, and they are ordered from largest to smallest (which is usually, but not necessarily, 1x1 pixel).
                // For cubemaps, the first surface is the main X+ face, followed by its mipmaps, followed by X- and its mipmaps, followed similarly by the Y and Z faces.
                // Volume textures are TBI.
                #endregion
            }
            return image;
        }
    }
}
