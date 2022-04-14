using System;
using Efilir.Core.Generics.Environment;
using Efilir.Core.Types;

namespace Efilir.Core.Cells
{
    [Flags]
    public enum WallType
    {
        Undefined = 2 << 0,

        Top = 2 << 1,
        Left = 2 << 2,
        Bottom = 2 << 3,
        Right = 2 << 4,
    }

    public class WallCell : IBaseCell
    {
        public WallCell(Coordinate position, WallType wallType)
        {
            Position = position;
            WallType = wallType;
        }

        public Coordinate Position { get; set; }
        public WallType WallType { get; set; }

        public void MakeTurn(IGenericGameArea gameArea)
        {
        }
    }
}