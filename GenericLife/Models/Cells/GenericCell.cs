﻿using System.Windows.Media;
using GenericLife.Declaration;
using GenericLife.Declaration.Cells;
using GenericLife.Tools;
using GenericLife.Types;

namespace GenericLife.Models.Cells
{
    public class GenericCell : ILiveCell
    {
        private int _health;

        public GenericCell()
        {
            CurrentRotate = new AngleRotation(0);
            Health = 100;
        }

        private ICellBrain _brain;
        public ICellBrain Brain
        {
            get => _brain;
            set
            {
                _brain = value;
                _brain.Cell = this;
            }
        }

        public AngleRotation CurrentRotate { get; set; }
        public ICellField FieldModel { get; set; }

        public int Health
        {
            get => _health;
            set => _health = value > 100 ? 100 : value;
        }

        public int Age { get; set; }

        public void TurnAction()
        {
            if (!IsAlive()) return;

            Brain.MakeTurn();
            IncreaseAge();
        }

        public bool IsAlive()
        {
            return Health > 0;
        }

        public FieldPosition Position { get; set; }

        public void MoveCommand(int commandRotate)
        {
            ActionCommand(commandRotate);

            var targetPosition = GetTargetPosition(commandRotate);
            var targetCell = FieldModel.GetCellOnPosition(targetPosition);

            if (targetCell == null) Position = targetPosition;
        }

        public void ActionCommand(int commandRotate)
        {
            var targetPosition = GetTargetPosition(commandRotate);
            var cellType = FieldModel.GetCellOnPosition(targetPosition)?.GetPointType() ?? PointType.Void;

            if (cellType == PointType.Food)
            {
                var cellOnWay = FieldModel.GetCellOnPosition(targetPosition);
                FieldModel.Foods.Remove(cellOnWay as FoodCell);
                Health += FoodCell.FoodHealthIncome;
                return;
            }

            if (cellType == PointType.Cell)
            {
                //Attack?
            }
        }

        public FieldPosition GetTargetPosition(int commandRotate)
        {
            var actualRotate = CurrentRotate + commandRotate;
            var newPosition = actualRotate.GetRotation();
            return Position + newPosition;
        }

        private void IncreaseAge()
        {
            Age++;
            Health--;
        }

        public override string ToString()
        {
            return $"Simple cell with {Health} health and {Age} age";
        }
    }
}