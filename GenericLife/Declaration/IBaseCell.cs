using System.Windows.Media;

namespace GenericLife.Declaration
{
    public interface IBaseCell
    {
        int PositionX { get; set; }
        int PositionY { get; set; }

        Color GetColor();
    }
}