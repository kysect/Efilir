using System.Collections.Generic;
using GenericLife.Interfaces;
using GenericLife.Tools;
using GenericLife.Types;

namespace GenericLife.Models.Cells
{
    public class CellBrain : ICellBrain
    {
        public IGenericCell Cell { private get; set; }
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

        public int Generation { get; set; }
        public int Breed { get; set; }
        public List<int> CommandList { get; }

        public void MakeTurn()
        {
            var recursionDeep = 0;
            var isTurnMade = false;

            while (recursionDeep < 64 && isTurnMade == false)
            {
                recursionDeep++;
                var commandId = CommandList[CurrentCommandIndex];
                isTurnMade = GenerateCommand(commandId);
            }
        }

        private bool GenerateCommand(int commandId)
        {
            //Command shift
            if (commandId >= 32)
            {
                CurrentCommandIndex += commandId;
                return false;
            }

            //Rotation
            if (commandId >= 24)
            {
                Cell.CurrentRotate += commandId;
                CurrentCommandIndex++;
                return false;
            }

            //Check
            if (commandId >= 16)
            {
                CommandShift(commandId);
                return false;
            }

            //Action
            if (commandId >= 8)
            {
                CommandShift(commandId);
                Cell.ActionCommand(commandId % 8);
                return true;
            }

            //Move
            CommandShift(commandId);
            Cell.MoveCommand(commandId % 8);
            return true;
        }

        private void CommandShift(int commandId)
        {
            var targetPosition = Cell.AnalyzePosition(commandId);
            var type = Cell.Field.GetCellOnPosition(targetPosition)?.GetPointType() ?? PointType.Void;
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
    }
}