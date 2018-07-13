using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GenericLife.Interfaces;
using GenericLife.Tools;

namespace GenericLife.Models
{
    public class PixelDrawer
    {
        private readonly int _size;
        private readonly WriteableBitmap _writableBitmap;

        public PixelDrawer(Image image)
        {
            _size = Configuration.FieldSize * Configuration.ScaleSize;
            image.Height = _size;
            image.Width = _size;

            _writableBitmap = new WriteableBitmap(
                _size,
                _size,
                12,
                12,
                PixelFormats.Bgr32,
                null);

            image.Source = _writableBitmap;
        }

        public void DrawPoints(IBaseCell[,] cells)
        {
            var pixels = new byte[_size, _size, 4];
            PrintBackgroundWithBlack(pixels);

            for (var y = 0; y < Configuration.FieldSize; y++)
            for (var x = 0; x < Configuration.FieldSize; x++)
            {
                var cell = cells[y, x];
                if (cell == null) continue;

                for (var addX = 0; addX < Configuration.ScaleSize; addX++)
                for (var addY = 0; addY < Configuration.ScaleSize; addY++)
                    PutPixel(pixels, cell.Position.X * Configuration.ScaleSize + addX,
                        cell.Position.Y * Configuration.ScaleSize + addY,
                        cell);
            }

            PrintPixels(pixels);
        }

        private void PutPixel(byte[,,] pixels, int positionX, int positionY, IBaseCell cell)
        {
            var color = CellColorGenerator.GetCellColor(cell);
            pixels[positionY, positionX, 0] = color.B;
            pixels[positionY, positionX, 1] = color.G;
            pixels[positionY, positionX, 2] = color.R;
        }

        private void PrintBackgroundWithBlack(byte[,,] pixels)
        {
            for (var row = 0; row < _size; row++)
            for (var col = 0; col < _size; col++)
            {
                for (var i = 0; i < 3; i++)
                    pixels[row, col, i] = 0;
                pixels[row, col, 3] = 255;
            }
        }

        private byte[] TransformTo1D(byte[,,] pixels)
        {
            var pixels1D = new byte[_size * _size * 4];

            var index = 0;
            for (var row = 0; row < _size; row++)
            for (var col = 0; col < _size; col++)
            for (var i = 0; i < 4; i++)
                pixels1D[index++] = pixels[row, col, i];

            return pixels1D;
        }

        private void PrintPixels(byte[,,] pixels)
        {
            var pixels1D = TransformTo1D(pixels);
            var rect = new Int32Rect(0, 0, _size, _size);
            var stride = 4 * _size;

            _writableBitmap.WritePixels(rect, pixels1D, stride, 0);
        }
    }
}