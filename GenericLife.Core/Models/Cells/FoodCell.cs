using GenericLife.Core.CellAbstractions;
using GenericLife.Core.Tools;
using GenericLife.Core.Types;

namespace GenericLife.Core.Models.Cells
{
    public class FoodCell : IFoodCell
    {
        public FoodCell(Coordinate position)
        {
            Position = position;
        }

        public Coordinate Position { get; set; }
        public GameArea Field { get; set; }
        public void MakeTurn()
        {
        }

        public int HealthIncome()
        {
            return Configuration.HealthIncomeFromFood;
        }
    }
}