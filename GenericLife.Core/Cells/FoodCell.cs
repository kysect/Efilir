using GenericLife.Core.Algorithms;
using GenericLife.Core.Tools;
using GenericLife.Core.Types;

namespace GenericLife.Core.Cells
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