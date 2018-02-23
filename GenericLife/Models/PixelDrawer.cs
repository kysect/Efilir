using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GenericLife.Declaration;
using GenericLife.Declaration.Cells;
using GenericLife.Tools;

namespace GenericLife.Models
{
    public class PixelDrawer : IPixelDrawer
    {
        private const int ScaleSize = Configuration.ScaleSize;
        private readonly WriteableBitmap _writableBitmap;
        private readonly int _size;
        private readonly byte[,,] _pixels;

        public PixelDrawer(Image image)
        {
            _size = Configuration.FieldSize * ScaleSize;
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
            _pixels = new byte[_size, _size, 4];
        }

        public void DrawPoints(IEnumerable<IBaseCell> cells)
        {
            //TODO: Check position update
            //TODO: Draw only if changed

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
            var color = CellColorGenerator.GetCellColor(cell);
            _pixels[positionY, positionX, 0] = color.B;
            _pixels[positionY, positionX, 1] = color.G;
            _pixels[positionY, positionX, 2] = color.R;
        }

        public void ClearBlack()
        {
            for (var row = 0; row < _size; row++)
            for (var col = 0; col < _size; col++)
            {
                for (int i = 0; i < 3; i++)
                    _pixels[row, col, i] = 0;
                _pixels[row, col, 3] = 255;
            }
        }

        private byte[] TransformTo1D()
        {
            var pixels1D = new byte[_size * _size * 4];

            var index = 0;
            for (var row = 0; row < _size; row++)
            for (var col = 0; col < _size; col++)
            for (var i = 0; i < 4; i++)
                pixels1D[index++] = _pixels[row, col, i];

            return pixels1D;
        }

        private void PrintPixels()
        {
            var pixels1D = TransformTo1D();
            var rect = new Int32Rect(0, 0, _size, _size);
            var stride = 4 * _size;

            _writableBitmap.WritePixels(rect, pixels1D, stride, 0);
        }
    }
}