using Efilir.Core.Cells;
using Efilir.Core.Environment;
using Efilir.Core.Generics.Environment;
using Efilir.Core.Tools;
using Efilir.Core.Types;

namespace Efilir.Core.PredefinedCells.Cells
{
    public class PredefinedCell : IBaseCell
    {
        public Coordinate Position { get; set; }
        public AngleRotation CurrentRotate { get; set; }

        private GameArea _gameArea;

        public PredefinedCell(GameArea gameArea, Coordinate position)
        {
            _gameArea = gameArea;
            Position = position;
            CurrentRotate = GetRandomRotation();
        }

        public void MakeTurn(IGenericGameArea gameArea)
        {
            Coordinate newPosition = Position + CurrentRotate.GetRotation();
            if (_gameArea.GetCellOnPosition(newPosition) is WallCell)
            {
                CurrentRotate = GetRandomRotation();
            }
            else
            {
                _gameArea.RemoveCell(this);
                Position = newPosition;
                _gameArea.AddCell(this);
            }
        }

        public override string ToString()
        {
            return $"PredefinedCell on {Position} with angle {CurrentRotate} age";
        }

        private AngleRotation GetRandomRotation()
        {
            return new AngleRotation(GlobalRand.Next(8));
        }
    }
}