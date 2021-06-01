using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Efilir.Core.Cells;

namespace Efilir.Client.Tools
{
    public class PixelDrawer
    {
        private int Size => _fieldSize * _scaleSize;
        private readonly int _fieldSize;
        private readonly int _scaleSize;

        private readonly WriteableBitmap _writableBitmap;

        public PixelDrawer(Image image, int fieldSize, int scaleSize)
        {
            _fieldSize = fieldSize;
            _scaleSize = scaleSize;
            image.Height = Size;
            image.Width = Size;

            _writableBitmap = new WriteableBitmap(
                Size,
                Size,
                12,
                12,
                PixelFormats.Bgr32,
                null);

            image.Source = _writableBitmap;
        }

        public void DrawPoints(IBaseCell[,] cells)
        {
            var pixels = new byte[Size, Size, 4];
            PrintBackgroundWithBlack(pixels);

            for (var y = 0; y < _fieldSize; y++)
                for (var x = 0; x < _fieldSize; x++)
                {
                    IBaseCell cell = cells[y, x];
                    if (cell == null) continue;

                    for (var addX = 0; addX < _scaleSize; addX++)
                        for (var addY = 0; addY < _scaleSize; addY++)
                            PutPixel(pixels,
                                cell.Position.X * _scaleSize + addX,
                                cell.Position.Y * _scaleSize + addY,
                                cell);
                }

            PrintPixels(pixels);
        }

        public void DrawPoints<T>(ICollection<T> cells) where T : IBaseCell
        {
            var pixels = new byte[Size, Size, 4];
            PrintBackgroundWithBlack(pixels);

            foreach (T cell in cells)
            {
                for (var addX = 0; addX < _scaleSize; addX++)
                for (var addY = 0; addY < _scaleSize; addY++)
                    PutPixel(pixels,
                        cell.Position.X * _scaleSize + addX,
                        cell.Position.Y * _scaleSize + addY,
                        cell);
            }

            PrintPixels(pixels);
        }

        private static void PutPixel(byte[,,] pixels, int positionX, int positionY, IBaseCell cell)
        {
            Color color = CellColorGenerator.GetCellColor(cell);
            pixels[positionY, positionX, 0] = color.B;
            pixels[positionY, positionX, 1] = color.G;
            pixels[positionY, positionX, 2] = color.R;
        }

        private void PrintBackgroundWithBlack(byte[,,] pixels)
        {
            for (var row = 0; row < Size; row++)
            {
                for (var col = 0; col < Size; col++)
                {
                    //for (var i = 0; i < 3; i++)
                    //    pixels[row, col, i] = 0;
                    pixels[row, col, 3] = 255;
                }
            }
        }

        private byte[] TransformTo1D(byte[,,] pixels)
        {
            var pixels1D = new byte[Size * Size * 4];

            var index = 0;
            for (var row = 0; row < Size; row++)
                for (var col = 0; col < Size; col++)
                    for (var i = 0; i < 4; i++)
                        pixels1D[index++] = pixels[row, col, i];

            return pixels1D;
        }

        //TODO: optimization is needed. We can always use 1D
        private void PrintPixels(byte[,,] pixels)
        {
            byte[] pixels1D = TransformTo1D(pixels);
            var rect = new Int32Rect(0, 0, Size, Size);
            int stride = 4 * Size;

            _writableBitmap.WritePixels(rect, pixels1D, stride, 0);
        }
    }
}