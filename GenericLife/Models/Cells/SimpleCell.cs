using System.Windows.Media;
using GenericLife.Declaration;
using GenericLife.Tools;

namespace GenericLife.Models.Cells
{
    public class SimpleCell : ILiveCell
    {
        private readonly ICellField _fieldModel;

        public SimpleCell(ICellField fieldService, FieldPosition position)
        {
            Position = position;
            _fieldModel = fieldService;

            Health = 100;
        }

        public int Health { get; set; }

        public FieldPosition Position { get; set; }
        public int Age { get; set; }

        public void TurnAction()
        {
            if (Health == 0)
                return;

            RandomMove();
        }

        public Color GetColor()
        {
            if (Health == 0)
                return CellColorGenerator.DeadCell();
            return CellColorGenerator.HealthIndicator(Health);
        }

        public void RandomMove()
        {
            Age += 1;
            int x, y;
            do
            {
                x = GlobalRand.Next(3) - 1;
                y = GlobalRand.Next(3) - 1;
            } while (x == 0 && y == 0);

            Move(Position + new FieldPosition(x, y));
        }

        private void Move(FieldPosition position)
        {
            var cellType = _fieldModel.GetPointType(position);
            Health -= 1;
            if (cellType == PointType.Food)
            {
                var cellOnWay = _fieldModel.GetCellOnPosition(position);
                _fieldModel.Foods.Remove(cellOnWay as FoodCell);
                Position = position;
                Health += FoodCell.FoodHealthIncome;

                return;
            }

            if (cellType == PointType.Void)
            {
                Position = position;
            }
        }

        public override string ToString()
        {
            return $"Simple cell with {Health} health and {Age} age";
        }
    }
}