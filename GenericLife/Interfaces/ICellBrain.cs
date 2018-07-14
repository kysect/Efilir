using System.Collections.Generic;

namespace GenericLife.Interfaces
{
    public interface ICellBrain
    {
        //TODO: implement
        int Generation { get; set; }
        int Breed { get; set; }
        List<int> CommandList { get; }
        void MakeTurn();
    }
}