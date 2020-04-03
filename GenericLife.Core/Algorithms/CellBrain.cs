using System.Collections.Generic;
using GenericLife.Core.Cells;
using GenericLife.Core.Tools;
using GenericLife.Core.Types;

namespace GenericLife.Core.Algorithms
{
    public class CellBrain : ICellBrain
    {
        private int _currentCommandIndex;

        public CellBrain(List<int> commandList)
        {
            CurrentCommandIndex = 0;
            CommandList = commandList;
        }

        private int CurrentCommandIndex
        {
            get => _currentCommandIndex;
            set => _currentCommandIndex = value % Configuration.CommandListSize;
        }

        public List<int> CommandList { get; }

        public void MakeTurn(IGenericCell cell, IGameArea gameArea)
        {
            var recursionDeep = 0;
            var isTurnMade = false;

            while (recursionDeep < 64 && !isTurnMade)
            {
                recursionDeep++;
                int commandId = CommandList[CurrentCommandIndex];
                isTurnMade = GenerateCommand(commandId, cell, gameArea);
            }
        }

        private bool GenerateCommand(int commandId, IGenericCell cell, IGameArea gameArea)
        {
            //TODO: fix
            if (commandId == 64)
            {
                CreateChild(cell, gameArea);
            }

            //Command shift
            if (commandId >= 32)
            {
                CurrentCommandIndex += commandId;
                return false;
            }

            //Rotation
            if (commandId >= 24)
            {
                cell.CurrentRotate += commandId;
                CurrentCommandIndex++;
                return false;
            }

            //Check
            if (commandId >= 16)
            {
                CommandShift(commandId, cell, gameArea);
                return false;
            }

            //Action
            if (commandId >= 8)
            {
                CommandShift(commandId, cell, gameArea);
                cell.ActionCommand(commandId % 8, gameArea);
                return true;
            }

            //Move
            CommandShift(commandId, cell, gameArea);
            cell.MoveCommand(commandId % 8, gameArea);
            return true;
        }

        private void CommandShift(int commandId, IGenericCell cell, IGameArea gameArea)
        {
            Coordinate targetPosition = cell.AnalyzePosition(commandId);
            PointType type = gameArea.GetCellOnPosition(targetPosition)?.GetPointType() ?? PointType.Void;
            var shift = 0;

            //TODO: More types
            if (type == PointType.Food)
                shift = 1;
            if (type == PointType.Void)
                shift = 2;
            if (type == PointType.Cell)
                shift = 3;
            if (type == PointType.Wall)
                shift = 4;

            CurrentCommandIndex += shift;
        }

        private void CreateChild(IGenericCell cell, IGameArea gameArea)
        {
            //TODO: replace const with gen parameter
            if (cell.Health > 60)
            {
                gameArea.TryCreateCellChild(cell);
            }
        }
    }
}