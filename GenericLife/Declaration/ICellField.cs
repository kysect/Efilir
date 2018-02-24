using System.Collections.Generic;
using GenericLife.Models.Cells;
using GenericLife.Types;

namespace GenericLife.Declaration
{
    public interface ICellField
    {
        IBaseCell GetCellOnPosition(FieldPosition position);
        void MakeCellsMove();

        void InitializeLiveCells(IEnumerable<ILiveCell> cellsList);
        bool AliveLessThanEight();
        void RemoveFoodCell(FoodCell cell);

        IEnumerable<IBaseCell> GetAllCells();
        List<ILiveCell> GetAllLiveCells();
    }
}