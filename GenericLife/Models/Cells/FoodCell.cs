using GenericLife.Interfaces;
using GenericLife.Tools;
using GenericLife.Types;

namespace GenericLife.Models.Cells
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