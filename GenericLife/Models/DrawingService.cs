using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GenericLife.Models
{
    public class DrawingService
    {
        private const int ScaleSize = 4;
        private readonly WriteableBitmap _writeableBitmap;
        public readonly int Height;
        public readonly int Width;
        private ListBox _logList;

        public DrawingService(Image image)
        {
            Height = (int) image.Height;
            Width = (int) image.Width;

            _writeableBitmap = new WriteableBitmap(
                Width,
                Height,
                96,
                96,
                PixelFormats.Bgr32,
                null);

            image.Source = _writeableBitmap;
        }

        public void DrawPoints(IEnumerable<SimpleCell> SimpleCells)
        {
            var pixels = new byte[Height, Width, 4];
            pixels = ClearBlack(pixels);

            foreach (var cell in SimpleCells)
                Parallel.For(0, ScaleSize,
                    addX =>
                    {
                        Parallel.For(0, ScaleSize, addY =>
                        {
                            PutPixel(pixels,
                                cell.PositionX * ScaleSize + addX,
                                cell.PositionY * ScaleSize + addY,
                                cell);
                        });
                    });

            PrintPixels(pixels);
        }

        public void PutPixel(byte[,,] pixels, int positionX, int positionY, SimpleCell cell)
        {
            pixels[positionY, positionX, 1] = cell.ColorState.G;
            pixels[positionY, positionX, 2] = cell.ColorState.R;
        }

        private byte[,,] ClearBlack(byte[,,] pixels)
        {
            for (var row = 0; row < Height; row++)
            for (var col = 0; col < Width; col++)
            {
                for (var i = 0; i < 3; i++)
                    pixels[row, col, i] = 0;
                pixels[row, col, 3] = 255;
            }

            return pixels;
        }

        private byte[] TransformTo1D(byte[,,] pixels)
        {
            var pixels1D = new byte[Height * Width * 4];

            var index = 0;
            for (var row = 0; row < Height; row++)
            for (var col = 0; col < Width; col++)
            for (var i = 0; i < 4; i++)
                pixels1D[index++] = pixels[row, col, i];

            return pixels1D;
        }

        private void PrintPixels(byte[,,] pixels)
        {
            var pixels1D = TransformTo1D(pixels);
            var rect = new Int32Rect(0, 0, Width, Height);
            var stride = 4 * Width;

            _writeableBitmap.WritePixels(rect, pixels1D, stride, 0);
        }
    }
}