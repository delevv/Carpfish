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

        // LakesController/All
        public const int LakesCountPerPage = 6;

        // TrophiesController/All
        public const int TrophiesCountPerPage = 6;
    }
}
