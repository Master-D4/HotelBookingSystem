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

    //Method For predict average price - for a given month
    public string PredictAveragePrice(string monthName)
    {
        if(!DateTime.TryParseExact(monthName, "MMMM", null, System.Globalization.DateTimeStyles.None, out DateTime parsedMonth))
            return "Sorry, I couldn't understand the month name";
        
        int month =  parsedMonth.Month;

        var prices = _store.RoomTypes
            .SelectMany(rt => _store.Bookings
                .Where(b => b.CheckIn.Month == month && b.RoomType == rt.Name)
                .Select(_ => rt.BasePrice));
        
        if(!prices.Any())
            return $"No past booking data available for {monthName}.";
            
        decimal avg = prices.Average();
        return $"The predicted average price in {monthName} is {avg:C}.";
    }
}