using System;
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
            if (position.X < 0
                || position.X >= _areaSize
                || position.Y < 0
                || position.Y >= _areaSize)
                return new WallCell
                {
                    Position = position
                };

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
                        case 1: AddCell(new WallCell { Position = new Coordinate(j, i) }); break;
                    }
                }
            }
        }

        public void GenerateRandomWall()
        {
            //TODO: random, heh
            for (var i = 0; i < _areaSize / 3; i++)
                AddCell(new WallCell { Position = new Coordinate(i, _areaSize / 3) });
        }
    }
}