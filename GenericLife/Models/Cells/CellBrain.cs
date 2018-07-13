using System.Collections.Generic;
using GenericLife.Interfaces;
using GenericLife.Types;

namespace GenericLife.Models.Cells
{
    public class CellBrain : ICellBrain
    {
        private readonly IGenericCell _cell;
        private int _command;

        public CellBrain(IGenericCell cell, List<int> commandList)
        {
            CurrentCommand = 0;
            CommandList = commandList;
            _cell = cell;
        }

        private int CurrentCommand
        {
            get => _command;
            //TODO: Fix this const
            set => _command = value % 64;
        }

        public List<int> CommandList { get; }

        public void MakeTurn()
        {
            var recursionDeep = 0;
            var isTurnMade = false;

            while (recursionDeep < 64 && isTurnMade == false)
            {
                recursionDeep++;
                var commandId = CommandList[CurrentCommand];
                isTurnMade = GenerateCommand(commandId);
            }

            if (recursionDeep == 64)
            {
                //Inactive cell
            }
        }

        private bool GenerateCommand(int commandId)
        {
            //Command shift
            if (commandId >= 32)
            {
                CurrentCommand += commandId;
                return false;
            }

            //Rotation
            if (commandId >= 24)
            {
                _cell.CurrentRotate += commandId;
                CurrentCommand++;
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
                _cell.ActionCommand(commandId % 8);
                return true;
            }

            //Move
            CommandShift(commandId);
            _cell.MoveCommand(commandId % 8);
            return true;
        }

        private void CommandShift(int commandId)
        {
            var targetPosition = _cell.AnalyzePosition(commandId);
            var type = _cell.Field.GetCellOnPosition(targetPosition)?.GetPointType() ?? PointType.Void;
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

            CurrentCommand += shift;
        }
    }
}