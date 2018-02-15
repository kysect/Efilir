using System.Windows.Media;
using GenericLife.Models;
using GenericLife.Types;

namespace GenericLife.Declaration
{
    public interface IBaseCell
    {
        FieldPosition Position { get; set; }
        ICellField FieldModel { get; set; }
        Color GetColor();
    }
}