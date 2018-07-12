using System;
using System.Collections.Generic;
using GenericLife.Declaration;
using GenericLife.Tools;

namespace GenericLife.Models.Cells
{
    public class CellBrain : ICellBrain
    {
        private int _command;

        public CellBrain()
        {
            CurrentCommand = 0;
        }

        public GenericCell Cell { get; set; }
        public ICellField Field { get; set; }
        public List<int> CommandList { get; set; }
        private int CurrentCommand
        {
            get => _command;
            set => _command = value % 64;
        }

        

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
                Cell.CurrentRotate = Cell.CurrentRotate + commandId;
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
            var commandRotation = Cell.CurrentRotate + commandId;
            commandRotation %= 8;
            var type = Field.GetPointType(Cell.Position + AngleRotation.GetRotation(commandRotation));
            var shift = 0;

            //TODO: More types
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