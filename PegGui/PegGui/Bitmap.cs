using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegGui
{
    struct PixelColour
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
    }

    class Bitmap
    {
        // Windows BITMAPINFOHEADER CompressionFormats:
        enum CompressionFormats {
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
        string headerType;
        int bmpSize;
        char reserved0;
        char reserved1;
        int startOffset;

        // DIB Header - as per https://en.wikipedia.org/wiki/BMP_file_format according to the Windows BITMAPINFOHEADER spec
        int headerSize;
        int width;
        int height;
        char colourPlanes;
        char bitsPerPixel;
        CompressionFormats compression;
        int imageSize;
        int horizontalDPI;
        int verticalDIP;
        int coloursInPallet;
        int importantColours;

        // Colour pallet
        List<PixelColour> pallet;

        // Used to store raw pixel data
        byte[] pixelData;


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
            this.imageSize = BitConverter.ToInt32(chopArray(b, 34, 4), 0);
            this.horizontalDPI = BitConverter.ToInt32(chopArray(b, 38, 4), 0);
            this.verticalDIP = BitConverter.ToInt32(chopArray(b, 42, 4), 0);
            this.coloursInPallet = BitConverter.ToInt32(chopArray(b, 46, 4), 0);
            this.importantColours = BitConverter.ToInt32(chopArray(b, 50, 4), 0);
        }


        private T[] chopArray<T>(T[] arr, int start, int len)
        {
            T[] rt = new T[len];

            Array.Copy(arr, start, rt, 0, len);

            return rt;
        }
    }
}
