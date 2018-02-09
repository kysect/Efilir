using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GenericLife.Declaration;

namespace GenericLife.Models
{
    public class ImageDrawingTool
    {
        private const int ScaleSize = 4;
        private readonly WriteableBitmap _writeableBitmap;
        public readonly int Height;
        public readonly int Width;
        private byte[,,] _pixels;

        public ImageDrawingTool(Image image)
        {
            Height = (int) image.Height;
            Width = (int) image.Width;
            _writeableBitmap = new WriteableBitmap(
                Width,
                Height,
                12,
                12,
                PixelFormats.Bgr32,
                null);

            image.Source = _writeableBitmap;
            _pixels = new byte[Height, Width, 4];
        }

        public void DrawPoints(IEnumerable<IBaseCell> cells)
        {
            foreach (var cell in cells)
                for (int addX = 0; addX < ScaleSize; addX++)
                {
                    for (int addY = 0; addY < ScaleSize; addY++)
                    {
                        PutPixel(cell.Position.X * ScaleSize + addX,
                            cell.Position.Y * ScaleSize + addY,
                            cell);
                    }
                }

            PrintPixels();
        }

        private void PutPixel(int positionX, int positionY, IBaseCell cell)
        {
            _pixels[positionY, positionX, 0] = cell.GetColor().B;
            _pixels[positionY, positionX, 1] = cell.GetColor().G;
            _pixels[positionY, positionX, 2] = cell.GetColor().R;
        }

        public void ClearBlack()
        {
            for (var row = 0; row < Height; row++)
            for (var col = 0; col < Width; col++)
            {
                for (int i = 0; i < 3; i++)
                    _pixels[row, col, i] = 0;
                _pixels[row, col, 3] = 255;
            }
        }

        private byte[] TransformTo1D()
        {
            var pixels1D = new byte[Height * Width * 4];

            var index = 0;
            for (var row = 0; row < Height; row++)
            for (var col = 0; col < Width; col++)
            for (var i = 0; i < 4; i++)
                pixels1D[index++] = _pixels[row, col, i];

            return pixels1D;
        }

        private void PrintPixels()
        {
            var pixels1D = TransformTo1D();
            var rect = new Int32Rect(0, 0, Width, Height);
            var stride = 4 * Width;

            _writeableBitmap.WritePixels(rect, pixels1D, stride, 0);
        }
    }
}