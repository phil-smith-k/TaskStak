using System.CommandLine.Parsing;
using System.Globalization;

namespace TaskStak.CLI.Utils
{
    public static class ParseArgument
    {
        public static DateOnly ParseDateArgument(ArgumentResult? result)
        {
            var today = DateTime.Today;

            if (result == null)
            {
                return DateOnly.FromDateTime(today);
            }

            var arg = result.Tokens.SingleOrDefault()?.Value;

            if (string.IsNullOrWhiteSpace(arg))
            {
                return DateOnly.FromDateTime(today);
            }

            if (TryParseDateArgument(arg, out var date))
            {
                return DateOnly.FromDateTime(date);
            }

            if (TryParseDayOfWeekArgument(arg, out var dayOfWeekDate))
            {
                return DateOnly.FromDateTime(dayOfWeekDate);
            }

            return DateOnly.MinValue; // Invalid date argument format, return a sentinel value
        }

        private static bool TryParseDateArgument(string str, out DateTime date)
        {
            date = default;

            if (DateTime.TryParseExact(str, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                return true;

            var safeFormats = new[]
            {
                "yyyy/MM/dd",    // Year first is never ambiguous
                "yyyyMMdd",      // Compact, unambiguous
                "yyyy.MM.dd",    // Dot notation, year first
                "dd-MMM-yyyy",   // With month name (e.g., 15-Jan-2025)
            };

            foreach (var format in safeFormats)
            {
                if (DateTime.TryParseExact(str, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                    return true;
            }

            if (DateTime.TryParse(str, out date))
                return true;

            return false;
        }

        private static bool TryParseDayOfWeekArgument(string str, out DateTime date)
        {
            var today = DateTime.Today;

            date = str.ToLowerInvariant() switch
            {
                Constants.Options.Today => today,
                Constants.Options.TodayAlias => today,

                Constants.Options.Tomorrow => today.AddDays(1),
                Constants.Options.TomorrowAlias => today.AddDays(1),

                Constants.Options.Monday => today.GetNextDayOfWeek(DayOfWeek.Monday),
                Constants.Options.MondayAlias => today.GetNextDayOfWeek(DayOfWeek.Monday),

                Constants.Options.Tuesday => today.GetNextDayOfWeek(DayOfWeek.Tuesday),
                Constants.Options.TuesdayAlias => today.GetNextDayOfWeek(DayOfWeek.Tuesday),

                Constants.Options.Wednesday => today.GetNextDayOfWeek(DayOfWeek.Wednesday),
                Constants.Options.WednesdayAlias => today.GetNextDayOfWeek(DayOfWeek.Wednesday),

                Constants.Options.Thursday => today.GetNextDayOfWeek(DayOfWeek.Thursday),
                Constants.Options.ThursdayAlias => today.GetNextDayOfWeek(DayOfWeek.Thursday),

                Constants.Options.Friday => today.GetNextDayOfWeek(DayOfWeek.Friday),
                Constants.Options.FridayAlias => today.GetNextDayOfWeek(DayOfWeek.Friday),

                Constants.Options.Saturday => today.GetNextDayOfWeek(DayOfWeek.Saturday),
                Constants.Options.SaturdayAlias => today.GetNextDayOfWeek(DayOfWeek.Saturday),

                Constants.Options.Sunday => today.GetNextDayOfWeek(DayOfWeek.Sunday),
                Constants.Options.SundayAlias => today.GetNextDayOfWeek(DayOfWeek.Sunday),

                _ => default,
            };

            return date != default;
        }
    }
}
