namespace Carpfish.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "Carpfish";

        public const string AdministratorRoleName = "Administrator";

        // Category
        public const int CategoryNameMaxLength = 40;

        // Item
        public const int ItemTitleMaxLength = 80;

        public const int ItemDescriptionMaxLength = 2000;

        public const int ItemMinPrice = 0;

        public const double ItemMaxPrice = 9999999999999999.99;

        // Lake
        public const int LakeNameMaxLength = 30;

        public const int LakeAreaMaxLength = 30;

        public const int LakeMinArea = 0;

        public const double LakeMaxArea = 9999999999999999.99;

        // Trophy
        public const double TrophyMinWieght = 0;

        public const double TrophyMaxWeight = 99.99;

        public const int TrophyBaitDescriptionMaxLength = 100;

        // Rig
        public const int RigNameMaxLength = 50;
        public const int RigDescriptionMaxLength = 500;

        // Step
        public const int StepDescriptionMaxLength = 2000;
        public const int StepNumberMinValue = 1;
        public const int StepNumberMaxValue = int.MaxValue;
        public const int StepRigIdMinValue = 1;
        public const int StepRigIdMaxValue = int.MaxValue;

        // Material
        public const int MaterialDescriptionMaxLength = 50;
        public const int MaterialNumberMinValue = 1;
        public const int MaterialNumberMaxValue = int.MaxValue;
        public const int MaterialRigIdMinValue = 1;
        public const int MaterialRigIdMaxValue = int.MaxValue;

        // Location
        public const double LocationLatitudeMinValue = -90.00;
        public const double LocationLatitudeMaxValue = +90.00;
        public const double LocationLongitudeMinValue = -180.00;
        public const double LocationLongitudeMaxValue = +180.00;

        // LakesController/All
        public const int LakesCountPerPage = 6;

        // TrophiesController/All
        public const int TrophiesCountPerPage = 6;

        // RigsController/All
        public const int RigsCountPerPage = 6;

        // Data seeding
        public const string AdminName = "admin@admin.bg";
        public const string AdminEmail = "admin@admin.bg";
        public const string AdminPass = "admin123456";
    }
}
