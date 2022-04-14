using System;
using System.Linq;
using Efilir.Core.Environment;
using Efilir.Core.PredefinedCells.Cells;
using Efilir.Core.Tools;
using Efilir.Core.Types;

namespace Efilir.Core.PredefinedCells
{
    public class PredefinedCellExecutionContext : IExecutionContext
    {
        private readonly IPixelDrawer _pd;
        private readonly PredefinedCellGameArea _gameArea;

        public PredefinedCellExecutionContext(IPixelDrawer pd)
        {
            _pd = pd;

            _gameArea = new PredefinedCellGameArea(Configuration.FieldSize);

            //for (int i = 0; i < 400; i++)
            //{
            //    var coordinate = new Vector(GlobalRand.Next(Configuration.FieldSize), GlobalRand.Next(Configuration.FieldSize));
            //    var predefinedCell = new PredefinedCell(
            //        _gameArea,
            //        coordinate,
            //        new Vector(0, 0));
            //    _gameArea.PredefinedCells.Add(predefinedCell);
            //}

            for (double degrees = 0; degrees < 360.0; degrees += 4)
            {
                var velocity = Vector.CreateFromAngle(Angle.FromDegree(degrees), Configuration.PredefinedCellVelocity);
                var position = new Vector(Configuration.FieldSize / 2.0, Configuration.FieldSize / 2.0);

                var predefinedCell = new AngleBasedPredefinedCell(_gameArea, position, velocity, PredefinedCellType.Red);
                _gameArea.PredefinedCells.Add(predefinedCell);
            }

            //var firstVelocity = Vector.CreateFromAngle(Angle.FromDegree(90), Configuration.PredefinedCellVelocity);
            //var firstPosition = new Vector(20, 20);
            //var first = new AngleBasedPredefinedCell(_gameArea, firstPosition, firstVelocity, PredefinedCellType.Red);

            //var secondVelocity = Vector.CreateFromAngle(Angle.FromDegree(0), Configuration.PredefinedCellVelocity);
            //var secondPosition = new Vector(30, 20);
            //var second = new AngleBasedPredefinedCell(_gameArea, secondPosition, secondVelocity, PredefinedCellType.Red);

            //_gameArea.PredefinedCells.Add(first);
            //_gameArea.PredefinedCells.Add(second);

            //for (int i = 0; i < 20; i++)
            //{
            //    int degree = GlobalRand.Next(360);
            //    var velocity = Vector.CreateFromAngle(Angle.FromDegree(degree), Configuration.PredefinedCellVelocity);

            //    var position = new Vector(GlobalRand.Next(Configuration.FieldSize), GlobalRand.Next(Configuration.FieldSize));

            //    var predefinedCell = new AngleBasedPredefinedCell(_gameArea, position, velocity, PredefinedCellType.Red);
            //    _gameArea.PredefinedCells.Add(predefinedCell);
            //}


        }

        public void OnRoundStart()
        {
        }

        public bool OnIterationStart()
        {
            _gameArea.PredefinedCells
                .AsParallel().ForAll(c => c.MakeTurn(null));
                //.ForEach(c => c.MakeTurn(null));
            _gameArea.UpdatePreviousPositions();
            return true;
        }

        public void OnRoundEnd()
        {
        }

        public void OnUiRender()
        {
            _pd.DrawPoints(_gameArea.PredefinedCells);
        }
    }
}