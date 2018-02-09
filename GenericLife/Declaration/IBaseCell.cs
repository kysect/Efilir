using System.Windows.Media;
using GenericLife.Models;

namespace GenericLife.Declaration
{
    public interface IBaseCell
    {
        FieldPosition Position { get; set; }

        Color GetColor();
    }
}