namespace Slobodianiuk.University.Github.Tracker.Core.Constants
{
    public static class ProjectConstants
    {
        public const int DISPLAY_DAYS = 12;
        public const int DISPLAY_LAST_DAYS = DISPLAY_DAYS - 1;
        public const string DATE_FORMAT = "yyyy-MM-dd";

        public const string USER_READABLE_DATE_FORMAT = "ddd, dd MMM";
        public const string USER_READABLE_SHORT_DATE_FORMAT = "dd MMM";

        public readonly static DateTime CHART_START_DATE = new DateTime(2024, 02, 10);
        public readonly static DateTime CHART_END_DATE = new DateTime(2024, 06, 23);
    }
}
