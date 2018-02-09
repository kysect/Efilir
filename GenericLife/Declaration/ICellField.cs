using System.Collections.Generic;
using GenericLife.Models;

namespace GenericLife.Declaration
{
    public interface ICellField
    {
        //TODO: Use interface
        List<ILiveCell> Cells { get; set; }
        List<FoodCell> Foods { get; set; }
        IBaseCell GetCellOnPosition(int positionX, int positionY);
        PointType GetPointType(int positionX, int positionY);

        void RandomMove();
    }
}