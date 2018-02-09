using System.Collections.Generic;
using GenericLife.Models;
using GenericLife.Models.Cells;

namespace GenericLife.Declaration
{
    public interface ICellField
    {
        //TODO: Use interface
        List<ILiveCell> Cells { get; set; }
        List<FoodCell> Foods { get; set; }
        IBaseCell GetCellOnPosition(FieldPosition position);
        PointType GetPointType(FieldPosition position);

        void RandomMove();
    }
}