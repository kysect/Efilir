﻿namespace Efilir.Core.Tools
{
    public static class Configuration
    {
        // Common
        public const int FieldSize = 150;
        public const int ScaleSize = 4;

        // Gen algo
        public const int FoodCount = 250;
        public const int HealthIncomeFromFood = 10;
        public const int CommandListSize = 64;
        public const int GenCount = 64;
        public const int GenMaxValue = 65;
        public const int LiveCellCount = 64;
        public const int TrapCount = 7;

        // Predefined
        public const double StartVelocity = 20;
        public const double CellVisibleAngle = 10.0 / 180;
        public const double MaxLengthForInteraction = 30;
        public const double WallPushCoefficient = 1;
        public const double RecalculationRoundDelta = 0.15;
        public const double PreviousStepCount = 5;
        public const double PredefinedCellVelocity = 10;


    }
}