using System.Collections.Generic;
using GenericLife.Models.Cells;
using GenericLife.Types;

namespace GenericLife.Declaration
{
    public interface ICellField
    {
        IBaseCell[,] CellsPosition { get; }

        void InitializeLiveCells(IEnumerable<IGeneticCell> cellsList);
        void DeleteAllElements();

        void MakeCellsMove();
        bool AliveLessThanEight();
        void RemoveFoodCell(FoodCell cell);

        IBaseCell GetCellOnPosition(FieldPosition position);
        IEnumerable<IBaseCell> GetAllCells();
        List<IGeneticCell> GetAllLiveCells();
    }
}