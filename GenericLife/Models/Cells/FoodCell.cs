using System.Windows.Media;
using GenericLife.Declaration;
using GenericLife.Tools;
using GenericLife.Types;

namespace GenericLife.Models.Cells
{
    public class FoodCell : IFoodCell
    {
        public FieldPosition Position { get; set; }
        public ICellField FieldModel { get; set; }
        public int HealthIncome()
        {
            return 10;
        }

        public FoodCell(FieldPosition position)
        {
            Position = position;
        }
    }
}