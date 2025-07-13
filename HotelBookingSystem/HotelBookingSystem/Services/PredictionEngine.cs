using System.Globalization;

namespace HotelBookingSystem.Services;

public class PredictionEngine
{
    private readonly MemoryStore _store;

    public PredictionEngine(MemoryStore store)
    {
        _store = store;
    }

    public decimal PredictAveragePrice(int? month = null, string? roomType = null)
    {
        var bookings = _store.Bookings.AsQueryable();
        
        if(month.HasValue)
            bookings = bookings.Where(b => b.CheckIn.Month == month.Value);

        if (!string.IsNullOrWhiteSpace(roomType))
            bookings = bookings.Where(b => b.RoomType.Equals(roomType, StringComparison.OrdinalIgnoreCase));

        if (!bookings.Any())
            return 0;

        var prices = new List<decimal>();
        foreach (var b in bookings)
        {
            var room = _store.RoomTypes.FirstOrDefault(rt => rt.Name.Equals(b.RoomType, StringComparison.OrdinalIgnoreCase));
            if (room != null)
                prices.Add(room.BasePrice);
        }
        
        return prices.Any() ? prices.Average() : 0;
    }

    public double PredictRoomAvailability(DateOnly startDate, DateOnly endDate, string? roomType = null)
    {
        var relevantBookings = _store.Bookings
            .Where(b => b.CheckIn <= endDate && b.CheckOut >= startDate);

        if (!string.IsNullOrWhiteSpace(roomType))
            relevantBookings =
                relevantBookings.Where(b => b.RoomType.Equals(roomType, StringComparison.OrdinalIgnoreCase));

        const int totalRooms = 10; // Currently set room cout as 10 for each type in the system
        return (double)relevantBookings.Count() / totalRooms;
    }

    public (DateOnly Start, DateOnly End) GetDateRangeFromPhrase(string phrase)
    {
        var today = DateTime.Today;
        if (phrase.Contains("next week"))
        {
            var start = today.AddDays(7 - (int)today.DayOfWeek);
            return (DateOnly.FromDateTime(start), DateOnly.FromDateTime(start.AddDays(6)));
        }
        if (phrase.Contains("this week"))
        {
            var start = today.AddDays(-(int)today.DayOfWeek);
            return (DateOnly.FromDateTime(start), DateOnly.FromDateTime(start.AddDays(6)));
        }
        
        // Default value set to today(value), if unknown
        var d = DateOnly.FromDateTime(today);
        return (d, d);
    }
}