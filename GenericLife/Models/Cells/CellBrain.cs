using System;
using System.Collections.Generic;
using GenericLife.Declaration;
using GenericLife.Tools;

namespace GenericLife.Models.Cells
{
    public class CellBrain
    {
        private readonly GenericCell _cell;
        private readonly ICellField _field;
        private int _command;

        public CellBrain(List<int> commandList, ICellField field, GenericCell cell)
        {
            CommandList = commandList;
            CurrentCommand = 0;
            _field = field;
            _cell = cell;
        }

        private int CurrentCommand
        {
            get => _command;
            set => _command = value % 64;
        }

        private List<int> CommandList { get; }

        public void MakeTurn()
        {
            int recursionDeep = 0;
            bool isTurnMade = false;

            while (recursionDeep < 64 && isTurnMade == false)
            {
                recursionDeep++;
                var commandId = CommandList[CurrentCommand];
                isTurnMade = GenerateCommand(commandId);
            }

            if (recursionDeep == 64)
            {
                _cell.IncreaseAge();
            }
        }

        private bool GenerateCommand(int commandId)
        {
            if (commandId < 0 || commandId >= 64)
                throw new ArgumentException();

            //Command shift
            if (commandId >= 32)
            {
                CurrentCommand += commandId - 32;
                return false;
            }

            //Rotation
            if (commandId >= 24)
            {
                _cell.CurrentRotate = _cell.CurrentRotate + commandId;
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
            var commandRotation = _cell.CurrentRotate + commandId;
            commandRotation %= 8;
            var type = _field.GetPointType(_cell.Position + AngleRotation.GetRotation(commandRotation));
            //TODO: Hm
            int shift = 5;

            if (type == PointType.Food)
                shift = 1;
            if (type == PointType.Void)
                shift = 2;
            if (type == PointType.Cell)
                shift = 3;
            if (type == PointType.OutOfRange)
                shift = 4;

            CurrentCommand += shift;
        }
    }
}