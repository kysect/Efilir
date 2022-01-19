using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Efilir.Core.Cells;
using Efilir.Core.Environment;

namespace Efilir.Client.Tools
{
    public class PixelDrawer : IPixelDrawer
    {
        private int Size => _fieldSize * _scaleSize;

        private readonly int _fieldSize;
        private readonly int _scaleSize;

        private readonly byte[] _pixels;

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

            _pixels = new byte[Size * Size * 4];
        }

        public void DrawPoints(IBaseCell[,] cells)
        {
            PrintBackgroundWithBlack();

            for (var y = 0; y < _fieldSize; y++)
                for (var x = 0; x < _fieldSize; x++)
                {
                    IBaseCell cell = cells[y, x];
                    if (cell == null) continue;

                    PutCell(cell);
                }

            PrintPixels();
        }

        public void DrawPoints<T>(ICollection<T> cells) where T : IBaseCell
        {
            PrintBackgroundWithBlack();

            foreach (T cell in cells)
                PutCell( cell);
            
            PrintPixels();
        }

        private void PutCell(IBaseCell cell)
        {
            for (var addX = 0; addX < _scaleSize; addX++)
                for (var addY = 0; addY < _scaleSize; addY++)
                    PutPixel(
                        cell.Position.X * _scaleSize + addX,
                        cell.Position.Y * _scaleSize + addY,
                        cell);
        }

        private void PutPixel(int positionX, int positionY, IBaseCell cell)
        {
            Color color = CellColorGenerator.GetCellColor(cell);
            var arrayPosition = (positionY * Size + positionX) * 4;

            _pixels[arrayPosition] = color.B;
            _pixels[++arrayPosition] = color.G;
            _pixels[++arrayPosition] = color.R;
        }

        private void PrintBackgroundWithBlack() =>
            Array.Clear(_pixels, 0, _pixels.Length);

        private void PrintPixels()
        {
            var rect = new Int32Rect(0, 0, Size, Size);
            int stride = 4 * Size;

            _writableBitmap.WritePixels(rect, _pixels, stride, 0);
        }
    }
}