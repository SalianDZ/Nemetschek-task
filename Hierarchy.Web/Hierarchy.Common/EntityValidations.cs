using System.Diagnostics;

namespace Hierarchy.Common
{
    public static class EntityValidations
    {
        public static class Employee
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 100;

            public const int ExperienceYearsMinValue = 0;
            public const int ExperienceYearsMaxValue = 50;
        }

        public static class Position
        {
            public const int TitleMinLength = 5;
            public const int TitleMaxLength = 100;

            public const int RankMinValue = 1;
            public const int RankMaxValue = 10;
        }

        public static class Department
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 100;

            public const int DescriptionMinLength = 5;
            public const int DescriptionMaxLength = 500;
        }
    }
}
