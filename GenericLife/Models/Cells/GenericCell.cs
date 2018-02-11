﻿using System.Collections.Generic;
using System.Windows.Media;
using GenericLife.Declaration;
using GenericLife.Tools;

namespace GenericLife.Models.Cells
{
    public class GenericCell : ILiveCell
    {
        private readonly CellFieldModel _fieldModel;
        private readonly CellBrain _brain;
        private int _currentRotation;

        public GenericCell(CellFieldModel fieldModel, FieldPosition position)
        {
            _fieldModel = fieldModel;
            Position = position;
            CurrentRotate = 0;
            Health = 100;
            _brain = new CellBrain(GlobalRand.GenerateCommandList(), _fieldModel, this);
        }

        public GenericCell(CellFieldModel fieldModel, FieldPosition position, CellBrain brain)
        {
            _fieldModel = fieldModel;
            Position = position;
            CurrentRotate = 0;
            Health = 100;
            _brain = brain;
        }

        public int CurrentRotate
        {
            get => _currentRotation;
            set => _currentRotation = value % 8;
        }

        private int _health;

        public int Health
        {
            get => _health;
            set => _health = value > 100 ? 100 : value;
        }

        public int Age { get; set; }

        public void TurnAction()
        {
            if (Health <= 0)
            {
                return;
            }

            _brain.MakeTurn();
            IncreaseAge();
        }

        public FieldPosition Position { get; set; }

        public Color GetColor()
        {
            if (Health <= 0)
                return CellColorGenerator.DeadCell();
            return CellColorGenerator.HealthIndicator(Health);
        }

        public int MoveCommand(int commandRotate)
        {
            ActionCommand(commandRotate);

            var newPos = GetCellOnWay(commandRotate);
            var directionCellState = _fieldModel.GetPointType(newPos);

            if (directionCellState == PointType.Void) Position = newPos;

            return 0;
        }

        public void ActionCommand(int commandRotate)
        {
            //IncreaseAge();

            var newPos = GetCellOnWay(commandRotate);
            var directionCellState = _fieldModel.GetPointType(newPos);

            if (directionCellState == PointType.Food)
            {
                var cellOnWay = _fieldModel.GetCellOnPosition(newPos);
                _fieldModel.Foods.Remove(cellOnWay as FoodCell);
                Health += FoodCell.FoodHealthIncome;
                return;
            }

            if (directionCellState == PointType.Cell)
            {
                //Attack?
            }
        }

        private FieldPosition GetCellOnWay(int commandRotate)
        {
            var actualRotate = (CurrentRotate + commandRotate) % 8;
            var newPosition = AngleRotation.GetRotation(actualRotate);
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

        public List<int> GetCommandList()
        {
            return _brain.CommandList;
        }
    }
}