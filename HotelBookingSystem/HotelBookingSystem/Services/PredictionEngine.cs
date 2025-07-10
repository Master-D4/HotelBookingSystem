namespace HotelBookingSystem.Services;

public class PredictionEngine
{
    private readonly MemoryStore _store;

    public PredictionEngine(MemoryStore store)
    {
        _store = store;
    }

    //Method for predict rooms availability - for a given month
    public string PredictAvailability(string monthName)
    {
        if (!DateTime.TryParseExact(monthName, "MMMM", null, System.Globalization.DateTimeStyles.None, out DateTime parsedMonth))
            return "Sorry, I couldn't understand the month name";
        
        int month =  parsedMonth.Month;

        int count = _store.Bookings.Count(b => b.CheckIn.Month == month);
        return count < 5
            ? $"Rooms are likely available in {monthName}."
            : $"Rooms may be limited in {monthName}. Consider booking early.";
    }

    //Method For predict average price - for a given month & room type
    public string PredictAveragePrice(string monthName, string? roomType = null)
    {
        if(!DateTime.TryParseExact(monthName, "MMMM", null, System.Globalization.DateTimeStyles.None, out DateTime parsedMonth))
            return "Hmm... Sorry, I couldn't understand the month name";
        
        int month =  parsedMonth.Month;
        
        var bookings = _store.Bookings.Where(b => b.CheckIn.Month == month);

        if (!string.IsNullOrEmpty(roomType))
        {
            bookings = bookings.Where(b => b.RoomType.Equals(roomType, StringComparison.OrdinalIgnoreCase));
        }
        
        var prices = bookings
            .Join(_store.RoomTypes,
                b => b.RoomType,
                rt => rt.Name,
                (b, rt) => rt.BasePrice);
        
        if(!prices.Any())
            return $"I couldn't find enough booking data for {(roomType ?? "any room")} in {monthName}.";
            
        decimal avg = prices.Average();
        return $"The average price for {(roomType ?? "all rooms")} in {monthName} is {avg:C}.";
    }
}