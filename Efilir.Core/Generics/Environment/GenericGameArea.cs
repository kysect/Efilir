using System;
using Efilir.Core.Cells;
using Efilir.Core.Environment;
using Efilir.Core.Generics.Cells;
using Efilir.Core.Types;

namespace Efilir.Core.Generics.Environment
{
    public class GenericGameArea : GameArea, IGenericGameArea
    {
        public GenericGameArea(int areaSize) : base(areaSize)
        {
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

        public bool TryCreateCellChild(IGenericCell cell)
        {
            foreach (Coordinate coordinate in cell.Position.EnumerateAround(cell.CurrentRotate))
            {
                if (GetCellOnPosition(coordinate) is null)
                {
                    var child = new GenericCell(cell.Brain) {Health = cell.Health / 3, Position = coordinate};
                    AddCell(child);
                    cell.Health /= 3;
                    return true;
                }
            }

            return false;
        }
    }
}