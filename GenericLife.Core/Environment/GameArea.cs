using System;
using System.Collections.Generic;
using System.Linq;
using GenericLife.Core.Algorithms;
using GenericLife.Core.Cells;
using GenericLife.Core.Tools;
using GenericLife.Core.Types;

namespace GenericLife.Core.Environment
{
    public class GameArea : IGameArea
    {
        public IBaseCell[,] Cells { get; private set; }

        public void CleanField()
        {
            Cells = new IBaseCell[Configuration.FieldSize, Configuration.FieldSize];
            GenerateRandomWall();
        }

        public IEnumerable<T> SelectIf<T>()
        {
            return Cells.Cast<IBaseCell>().Where(cell => cell is T).Cast<T>();
        }

        public void AddCell(IBaseCell cell)
        {
            Cells[cell.Position.Y, cell.Position.X] = cell;
        }

        public IBaseCell GetCellOnPosition(Coordinate position)
        {
            if (position.X < 0
                || position.X >= Configuration.FieldSize
                || position.Y < 0
                || position.Y >= Configuration.FieldSize)
                return new WallCell
                {
                    Position = position
                };

            return Cells[position.Y, position.X];
        }

        public void TryEat(IGenericCell sender, Coordinate foodPosition)
        {
            IBaseCell cellOnWay = GetCellOnPosition(foodPosition);
            PointType cellType = cellOnWay.GetPointType();
            if (cellType != PointType.Food)
                throw new ArgumentException();

            sender.Health += ((FoodCell)cellOnWay).HealthIncome();
            RemoveCell(cellOnWay);
        }

        public void RemoveCell(IBaseCell cell)
        {
            Cells[cell.Position.Y, cell.Position.X] = null;
        }

        public Coordinate GetEmptyPosition()
        {
            Coordinate newPos;
            do
            {
                newPos = GlobalRand.GeneratePosition(Configuration.FieldSize);
            } while (GetCellOnPosition(newPos) != null);

            return newPos;
        }

        private void GenerateRandomWall()
        {
            //TODO: random, heh
            for (var i = 0; i < Configuration.FieldSize / 3; i++)
                AddCell(new WallCell {Position = new Coordinate(i, Configuration.FieldSize / 3)});
        }
    }
}