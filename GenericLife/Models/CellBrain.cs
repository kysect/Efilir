using System;
using System.Collections.Generic;
using GenericLife.Declaration;
using GenericLife.Tools;

namespace GenericLife.Models
{
    public class CellBrain
    {
        private int _command;
        private int CurrentCommand
        {
            get => _command;
            set => _command = value % 64;
        }

        private ICellField _field;
        private GenericCell _cell;
        public CellBrain(List<int> commandList, ICellField field, GenericCell cell)
        {
            CommandList = commandList;
            CurrentCommand = 0;
            _field = field;
            _cell = cell;
        }

        private List<int> CommandList { get; set; }

        public void GenerateCommand(int commandId)
        {
            if (commandId < 0 || commandId >= 64)
                throw new ArgumentException();

            //Command shift
            if (commandId >= 32)
            {
                CurrentCommand += (commandId - 32);
                return;
            }
            
            //Rotation
            if (commandId >= 24)
            {
                _cell.CurrentRotate = _cell.CurrentRotate + commandId;
                return;
            }

            //Check
            if (commandId >= 16)
            {
                int commandRotation = _cell.CurrentRotate + commandId;
                PointType type = _field.GetPointType(_cell.Position + AngleRotation.GetRotation(commandRotation));
                int shift = CommandShift(type);
                CurrentCommand += shift;
            }

            //Action
            if (commandId >= 8)
            {

            }
        }

        private static int CommandShift(PointType type)
        {
            if (type == PointType.Food)
                return 1;
            if (type == PointType.Void)
                return 2;
            if (type == PointType.Cell)
                return 3;
            if (type == PointType.OutOfRange)
                return 4;

            //TODO: Error?
            return 5;
        }
    }
}