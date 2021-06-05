using Efilir.Core.Types;

namespace Efilir.Core.Tools
{
    public static class Configuration
    {
        // Common
        public const int FieldXSize = 150;
        public const int FieldYSize = 100;
        public const int FieldSize = 100;
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
        public static Angle CellVisibleAngle = Angle.FromDegree(60.0);
        public const double MaxLengthForInteraction = 20;
        public const double WallPushCoefficient = 1;
        public const double RecalculationRoundDelta = 0.15;
        public const double PreviousStepCount = 20;
        public const double PredefinedCellVelocity = 5;
        public static Angle TurnAngleChange = Angle.FromDegree(10.0);
    }
}