using System;

namespace HotelBookingSystem.Helpers
{
    public static class Utils
    {
        /* Return the start week of given date, base on given first day of week */
        public static DateTime StartOfWeek(this DateTime date, DayOfWeek startOfWeek)
        {
            int diff = (7 + (date.DayOfWeek - startOfWeek)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        /* Check two DateOnly values within the same week (Monday-Sunday) */
        public static bool IsInSameWeek(DateOnly d1, DateOnly d2)
        {
            var start1 = d1.ToDateTime(TimeOnly.MinValue).StartOfWeek(DayOfWeek.Monday);
            var start2 = d2.ToDateTime(TimeOnly.MinValue).StartOfWeek(DayOfWeek.Monday);
            return start1 == start2;
        }
        
        /* Convert DateOnly type to string (Monday, Jul 01) */
        public static string ToFormattedDate(this DateTime date)
        {
            return $"{date:dddd, MMM dd}";
        }
        
        /* Convert DateOnly type to string (01/07/2025) */
        public static string ToFormattedDyd(this DateTime date)
        {
            return $"{date: dd/MM/yyy}";
        }
        
        /* Convert DateOnly type to string (01/07/2025) */
        public static string ToDayDateFormat(this DateOnly date)
        {
            return date.ToString("dddd, dd/MMM");
        }
    }
}