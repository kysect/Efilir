using GenericLife.Types;

namespace GenericLife.Declaration
{
    public interface IBaseCell
    {
        FieldPosition Position { get; set; }
        ICellField FieldModel { get; set; }
    }
}