using Efilir.Core.Cells;
using Efilir.Core.Tools;
using Efilir.Core.Types;

namespace Efilir.Core.Environment
{
    public class GameArea
    {
        public IBaseCell[,] Cells { get; private set; }

        public GameArea(int areaSize)
        {
            _areaSize = areaSize;
            CleanField();
        }

        private readonly int _areaSize;

        public void CleanField()
        {
            Cells = new IBaseCell[_areaSize, _areaSize];
        }

        public void AddCell(IBaseCell cell)
        {
            Cells[cell.Position.Y, cell.Position.X] = cell;
        }

        public IBaseCell GetCellOnPosition(Coordinate position)
        {
            WallType result = WallType.Undefined;

            if (position.X < 0)
                result |= WallType.Left;

            if (position.X >= _areaSize)
                result |= WallType.Right;

            if (position.Y < 0)
                result |= WallType.Bottom;

            if (position.Y >= _areaSize)
                result |= WallType.Top;

            if (result != WallType.Undefined)
                return new WallCell(position, result);

            return Cells[position.Y, position.X];
        }

        public void RemoveCell(IBaseCell cell)
        {
            RemoveCell(cell.Position);
        }

        public void RemoveCell(Coordinate position)
        {
            Cells[position.Y, position.X] = null;
        }

        public Coordinate GetEmptyPosition()
        {
            Coordinate newPos;
            do
            {
                newPos = GlobalRand.GeneratePosition(_areaSize);
            } while (GetCellOnPosition(newPos) != null);

            return newPos;
        }

        public void GenerateGameField(int[,] cells)
        {
            //TODO: random, heh
            for (var i = 0; i < Configuration.FieldSize; i++)
            {
                for (var j = 0; j < Configuration.FieldSize; j++)
                {
                    switch (cells[i, j])
                    {
                        case 1: AddCell(new WallCell(new Coordinate(j, i), WallType.Undefined)); break;
                    }
                }
            }
        }

        public void GenerateRandomWall()
        {
            //TODO: random, heh
            for (var i = 0; i < _areaSize / 3; i++)
                AddCell(new WallCell(new Coordinate(i, _areaSize / 3), WallType.Undefined));
        }
    }
}