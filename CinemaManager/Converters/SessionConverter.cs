namespace CinemaManager.Converters
{
    public static class SessionConverter
    {
        public static TimeSpan GetEndTimeFrom(string duration)
        {
            var output = duration.Split(" ");

            string extractHour;
            string extractMinutes;

            if (output.Length > 1)
            {
                extractHour = output[0].Replace("h", " ");
                extractMinutes = output[1].Replace("min", " ");
            }
            else
            {
                extractHour = output[0].Replace("h", " ");
                if (extractHour == duration) { extractHour = "0"; }

                extractMinutes = output[0].Replace("min", " ");
                if (extractMinutes == duration) { extractMinutes = "0"; }
            }

            var endTimeHour = TimeSpan.FromHours(double.Parse(extractHour));
            var endTimeMinutes = TimeSpan.FromMinutes(double.Parse(extractMinutes));

            return new TimeSpan(endTimeHour.Hours, endTimeMinutes.Minutes, 0);
        }
    }
}