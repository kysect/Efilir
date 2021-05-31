using Efilir.Core.Cells;
using Efilir.Core.Generics.Algorithms;
using Efilir.Core.Tools;
using Efilir.Core.Types;

namespace Efilir.Core.Generics.Cells
{
    public class FoodCell : IBaseCell
    {
        public FoodCell(Coordinate position)
        {
            Position = position;
        }

        public Coordinate Position { get; set; }
        public void MakeTurn(IGameArea gameArea)
        {
            // Do nothing
        }

        public int HealthIncome()
        {
            return Configuration.HealthIncomeFromFood;
        }
    }
}