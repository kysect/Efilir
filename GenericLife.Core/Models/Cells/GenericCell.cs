using GenericLife.Core.Algorithms;
using GenericLife.Core.CellAbstractions;
using GenericLife.Core.Types;

namespace GenericLife.Core.Models.Cells
{
    public class GenericCell : IGenericCell
    {
        public GenericCell(ICellBrain brain)
        {
            CurrentRotate = new AngleRotation(0);
            Health = 100;
            Brain = brain;
        }

        public ICellBrain Brain { get; }
        public AngleRotation CurrentRotate { get; set; }
        public GameArea Field { get; set; }
        public int Health { get; private set; }
        public int Age { get; private set; }
        public Coordinate Position { get; set; }

        public void MakeTurn()
        {
            if (!IsAlive())
                return;

            Brain.MakeTurn(this);
            IncreaseAge();
        }

        public bool IsAlive()
        {
            return Health > 0;
        }

        public void MoveCommand(int commandRotate)
        {
            ActionCommand(commandRotate);
            
            Coordinate targetPosition = AnalyzePosition(commandRotate);
            IBaseCell targetCell = Field.GetCellOnPosition(targetPosition);

            if (targetCell == null)
                Position = targetPosition;
        }

        public void ActionCommand(int commandRotate)
        {
            //TODO: return cell, not only type
            Coordinate targetPosition = AnalyzePosition(commandRotate);
            IBaseCell cellOnWay = Field.GetCellOnPosition(targetPosition);
            if (cellOnWay == null)
            {
                return;
            }

            PointType cellType = cellOnWay.GetPointType();
            if (cellType == PointType.Food)
            {
                Health += ((FoodCell)cellOnWay).HealthIncome();
                Field.RemoveCell(cellOnWay);
                return;
            }

            if (cellType == PointType.Cell)
            {
                //Attack?
            }
        }

        public Coordinate AnalyzePosition(int commandRotate)
        {
            AngleRotation actualRotate = CurrentRotate + commandRotate;
            Coordinate newPosition = actualRotate.GetRotation();
            return Position + newPosition;
        }

        public override string ToString()
        {
            return $"G0 with {Health} health and {Age} age";
        }

        private void IncreaseAge()
        {
            Age++;
            Health--;
        }
    }
}