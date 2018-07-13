using GenericLife.Models;
using GenericLife.Types;

namespace GenericLife.Interfaces
{
    public interface IBaseCell
    {
        Coordinate Position { get; set; }
        GameArea Field { get; set; }
    }
}