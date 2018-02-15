using System.Collections.Generic;
using GenericLife.Models;
using GenericLife.Models.Cells;
using GenericLife.Types;

namespace GenericLife.Declaration
{
    public interface ICellField
    {
        List<ILiveCell> Cells { get; set; }
        List<FoodCell> Foods { get; set; }
        IBaseCell GetCellOnPosition(FieldPosition position);
        //TODO: static method? 
        PointType GetPointType(FieldPosition position);

        void RandomMove();

        void AddCell(ILiveCell cell);
    }
}