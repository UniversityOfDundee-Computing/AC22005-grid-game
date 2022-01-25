using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegGui
{
    // Store Pixel colour data for use with the Bitmap class
    struct PixelColour : IEquatable<PixelColour>
    {
        byte red { get; set; }
        byte green {get; set;}
        byte blue {get; set;}
        byte alpha {get; set;}

        public PixelColour(byte red, byte green, byte blue, byte alpha)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
            this.alpha = alpha;
        }

        public bool Equals(PixelColour other)
        {
            return (this.red == other.red && this.green == other.green && this.blue == other.blue && this.alpha == other.alpha);
        }
    }

    class Bitmap
    {
        // Windows BITMAPINFOHEADER CompressionFormats:
        public enum CompressionFormats {
            BI_RGB = 0,
            BI_RLE8 = 1,
            BI_RLE4 = 2,
            BI_BITFIELDS = 3,
            BI_JPEG = 4,
            BI_PNG = 5,
            BI_ALPHABITFIELDS = 6,
            BI_CMYK = 11,
            BI_CMYKRLE8 = 12,
            BI_CMYKRLE4 = 13
        };


        // BMP file Header - as per https://en.wikipedia.org/wiki/BMP_file_format
        public string headerType { get; set; }
        public int bmpSize {get; set;}
        public char reserved0 {get; set;}
        public char reserved1 {get; set;}
        public int startOffset {get; set;}

        // DIB Header - as per https://en.wikipedia.org/wiki/BMP_file_format according to the Windows BITMAPINFOHEADER spec
        public int headerSize {get; set;}
        public int width { get; set; }
        public int height { get; set; }
        public char colourPlanes {get; set;}
        public char bitsPerPixel {get; set;}
        public CompressionFormats compression {get; set;}
        public int imageSize {get; set;}
        public int horizontalDPI {get; set;}
        public int verticalDIP {get; set;}
        public int coloursInPallet {get; set;}
        public int importantColours {get; set;}

        // Colour pallet
        public List<PixelColour> pallet = new List<PixelColour>();

        // Used to store raw pixel data
        public PixelColour[,] pixelData { get; set; }


        public Bitmap(Byte[] b)
        {
            parseBinary(b);
        }

        private void parseBinary(Byte[] b)
        {
            // BMP file Header - as per https://en.wikipedia.org/wiki/BMP_file_format
            this.headerType = Encoding.ASCII.GetString(chopArray(b, 0, 2));
            if (this.headerType != "BM")
            {
                throw new Exception("Unrecognised Bitmap Format - Only modern Windows bitmaps are supported (BM header)");
            }
            this.bmpSize = BitConverter.ToInt32(chopArray(b, 2, 4), 0);
            this.reserved0 = BitConverter.ToChar(chopArray(b, 6, 2), 0);
            this.reserved1 = BitConverter.ToChar(chopArray(b, 8, 2), 0);
            this.startOffset = BitConverter.ToInt32(chopArray(b, 10, 4), 0);

            // DIB Header - as per https://en.wikipedia.org/wiki/BMP_file_format according to the Windows BITMAPINFOHEADER spec
            this.headerSize = BitConverter.ToInt32(chopArray(b, 14, 4), 0);

            if (this.headerSize != 40)
            {
                throw new Exception("Unrecognised Bitmap Format - Only modern Windows bitmaps are supported (BITMAPINFOHEADER / 40 byte header)");
            }
            this.width = BitConverter.ToInt32(chopArray(b, 18, 4), 0);
            this.height = BitConverter.ToInt32(chopArray(b, 22, 4), 0);
            this.colourPlanes = BitConverter.ToChar(chopArray(b, 26, 2), 0);
            this.bitsPerPixel = BitConverter.ToChar(chopArray(b, 28, 2), 0);
            this.compression = (CompressionFormats) BitConverter.ToInt32(chopArray(b, 30, 4), 0);
            
            if (this.compression != CompressionFormats.BI_RGB)
            {
                throw new Exception("Unrecognised Bitmap Format - Only Uncompressed bitmaps are supported");
            }

            this.imageSize = BitConverter.ToInt32(chopArray(b, 34, 4), 0);
            this.horizontalDPI = BitConverter.ToInt32(chopArray(b, 38, 4), 0);
            this.verticalDIP = BitConverter.ToInt32(chopArray(b, 42, 4), 0);
            this.coloursInPallet = BitConverter.ToInt32(chopArray(b, 46, 4), 0);
            this.importantColours = BitConverter.ToInt32(chopArray(b, 50, 4), 0);

            // Initialise a store for the pixel data
            this.pixelData = new PixelColour[height, width];

            // Scann the colour pallet if less than 16 bpp
            if (this.bitsPerPixel <= 8)
            {
                // Known pallet start offset
                int i = 0x36;
                while (i < this.startOffset)
                {
                    // Colours to pallet in BGRA order
                    this.pallet.Add(
                        new PixelColour(
                            b[i+2],
                            b[i+1],
                            b[i+0],
                            b[i+3]
                            )
                        );
                    i += 4;
                }
            }

            int rowSize = (int)(Math.Round(Math.Ceiling((double)((this.bitsPerPixel * this.width+31) / 32)) * 4)); // calculate the length of a row - account for padding

            // Here, j, k, l are all itterators to itterate over sections of the bytes, j ittereates over bitmap rows, k ittereates over bytes in a row, l over bits in a byte.
            int j = this.startOffset;
            int _Y = 0;
            while (j < b.Length && _Y < this.height)
            {
                int _X = 0;
                switch (this.bitsPerPixel)
                {
                    case (char)1: // Tested - Works 25/01/22 Adam Mathieson
                        while (_X < this.width) // While we have not completed a row
                        {
                            for (int k = 0; k < rowSize && _X < this.width; k++) // Itterate over the bytes in a row
                            {
                                for (int l = 0; l < 8 && _X < this.width; l++) // Itterate over the bits in the byte
                                {
                                    pixelData[this.height-1-_Y, _X] = pallet[(int)Math.Min(b[j + k] & (1 << (7-l)), 1)]; // Cast the bit to a number to lookup in the pallet
                                    _X++;
                                }
                            }
                        }
                        break;
                    case (char)2: // Unsure if this bit works, dont have any 2 bit encoded images
                        while (_X < this.width) // While we have not completed a row
                        {
                            for (int k = 0; k < rowSize && _X < this.width; k++) // Itterate over the bytes in a row
                            {
                                int counter = 0;
                                for (int l = 0; l < 8 && _X < this.width; l++) // Itterate over the bits in the byte
                                {
                                    if (l+1 % 2 == 0) // If l+1 is even add 2 and add to pixel data based on palleted colour counter
                                    {
                                        if ((int)(b[j + k] & (1 << l)) != 0)
                                            counter += 2;
                                        pixelData[this.height-1 - _Y, _X] = pallet[counter];
                                        _X++;
                                        counter = 0;
                                    } else
                                    {
                                        if ((int)(b[j + k] & (1 << l)) != 0)
                                            counter = 1;
                                    }
                                }
                            }
                        }
                        break;
                    case (char)4: // Tested - Works 25/01/22 Adam Mathieson
                        while (_X < this.width) // While we have not completed a row
                        {
                            for (int k = 0; k < rowSize && _X < this.width; k++) // Itterate over the bytes in a row
                            {
                                int counter = 0;
                                for (int l = 0; l < 8 && _X < this.width+1; l++) // Itterate over the bits in the byte
                                {
                                    if (l == 3 || l == 7) // If end of nibble, add 8 to counter and push to the pixeldata array
                                    {
                                        if ((int)(b[j + k] & (1 << l)) == 1)
                                            counter += 8;
                                        if ((l == 3 && _X + 1 < this.width))
                                            pixelData[this.height-1 - _Y, _X+1] = pallet[counter];
                                    
                                        if (l == 3 && _X == this.width-1)
                                            pixelData[this.height-1 - _Y, _X] = pallet[counter];

                                        if (l == 7 && _X <= this.width)
                                            pixelData[this.height-1 - _Y, _X-1] = pallet[counter];
                                        _X++;
                                        counter = 0;
                                    } else
                                    {
                                        if ((int)(b[j + k] & (1 << l)) != 0) // if bit set add approppriate value to counter
                                        {
                                            switch (l)
                                            {
                                                case 0:
                                                case 4:
                                                    counter += 1;
                                                    break;
                                                case 1:
                                                case 5:
                                                    counter += 2;
                                                    break;
                                                case 2:
                                                case 6:
                                                    counter += 4;
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case (char)8:
                        throw new NotImplementedException();
                        break;
                    case (char)16:
                        throw new NotImplementedException();
                        break;
                    case (char)24:
                        throw new NotImplementedException();
                        break;
                    default:
                        throw new Exception("Unrecognised Bitmap Format - Invalid bit depth (must be 1, 2, 4, 8, 16, 24)");
                }

                j += rowSize;
                _Y++;
            }

        }

        // Substr for arrays
        private T[] chopArray<T>(T[] arr, int start, int len)
        {
            T[] rt = new T[len];

            Array.Copy(arr, start, rt, 0, len);

            return rt;
        }
    }
}
