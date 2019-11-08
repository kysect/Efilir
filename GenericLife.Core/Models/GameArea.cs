using System.Collections.Generic;
using System.Linq;
using GenericLife.Core.CellAbstractions;
using GenericLife.Core.Models.Cells;
using GenericLife.Core.Tools;
using GenericLife.Core.Types;

namespace GenericLife.Core.Models
{
    public class GameArea
    {
        public GameArea()
        {
            CleanField();
        }

        public IBaseCell[,] Cells { get; private set; }

        public void CleanField()
        {
            Cells = new IBaseCell[Configuration.FieldSize, Configuration.FieldSize];
            GenerateRandomWall();
        }

        public IEnumerable<T> Select<T>()
        {
            return Cells.Cast<IBaseCell>().Where(cell => cell is T).Cast<T>();
        }

        public void AddCell(IBaseCell cell)
        {
            Cells[cell.Position.Y, cell.Position.X] = cell;
        }

        public IBaseCell GetCellOnPosition(Coordinate position)
        {
            if (position.X < 0 || position.X >= Configuration.FieldSize
                               || position.Y < 0
                               || position.Y >= Configuration.FieldSize)
                return new WallCell
                {
                    Position = position
                };

            return Cells[position.Y, position.X];
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
            for (var i = 0; i < Configuration.FieldSize / 3; i++)
                AddCell(new WallCell {Field = this, Position = new Coordinate(i, Configuration.FieldSize / 3)});
        }
    }
}