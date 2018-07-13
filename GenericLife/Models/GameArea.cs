using System.Linq;
using GenericLife.Interfaces;
using GenericLife.Models.Cells;
using GenericLife.Tools;
using GenericLife.Types;

namespace GenericLife.Models
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

        public int CurrentTypeCount<T>()
        {
            return Cells.Cast<IBaseCell>().Count(cell => cell is T);
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

        public void RemoveCell(Coordinate position)
        {
            Cells[position.Y, position.X] = null;
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