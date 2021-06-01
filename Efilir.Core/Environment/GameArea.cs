using System.Collections.Generic;
using Efilir.Core.Cells;
using Efilir.Core.PredefinedCells.Cells;
using Efilir.Core.Tools;
using Efilir.Core.Types;

namespace Efilir.Core.Environment
{
    public class GameArea
    {
        public IBaseCell[,] Cells { get; private set; }

        public GameArea(int areaSize)
        {
            AreaSize = areaSize;
            CleanField();
        }

        public int AreaSize { get; }

        public void CleanField()
        {
            Cells = new IBaseCell[AreaSize, AreaSize];
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

            if (position.X >= AreaSize)
                result |= WallType.Right;

            if (position.Y < 0)
                result |= WallType.Bottom;

            if (position.Y >= AreaSize)
                result |= WallType.Top;

            if (result != WallType.Undefined)
                return new WallCell(position, result);

            return Cells[position.Y, position.X];
        }

        public WallType GetWallType(Vector position)
        {
            WallType result = WallType.Undefined;

            if (position.X < 0)
                result |= WallType.Left;

            if (position.X >= AreaSize)
                result |= WallType.Right;

            if (position.Y < 0)
                result |= WallType.Bottom;

            if (position.Y >= AreaSize)
                result |= WallType.Top;

            return result;
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
                newPos = GlobalRand.GeneratePosition(AreaSize);
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
            for (var i = 0; i < AreaSize / 3; i++)
                AddCell(new WallCell(new Coordinate(i, AreaSize / 3), WallType.Undefined));
        }
    }
}