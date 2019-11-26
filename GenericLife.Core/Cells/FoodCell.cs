using GenericLife.Core.Environment;
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
        public GameArea Field { get; set; }
        public void MakeTurn()
        {
            // Do nothing
        }

        public int HealthIncome()
        {
            return Configuration.HealthIncomeFromFood;
        }
    }
}