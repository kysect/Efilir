using System.Collections.Generic;
using GenericLife.Models.Cells;
using GenericLife.Types;

namespace GenericLife.Declaration
{
    public interface ICellField
    {
        List<ILiveCell> Cells { get; set; }
        List<FoodCell> Foods { get; set; }
        IBaseCell GetCellOnPosition(FieldPosition position);
        void MakeCellsMove();

        void AddCell(ILiveCell cell);
        void AddCell(IEnumerable<ILiveCell> cellsList);
        bool AliveLessThanEight();
    }
}