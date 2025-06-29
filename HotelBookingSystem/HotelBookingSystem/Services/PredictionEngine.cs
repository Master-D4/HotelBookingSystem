namespace HotelBookingSystem.Services;

public class PredictionEngine
{
    private readonly MemoryStore _store;
    
    public PredictionEngine(MemoryStore store) => _store = store;

    //Method For Predict Average Price
    public decimal PredictAveragePrice(int month)
    {
        var booking = _store.Bookings.Where(b => b.CheckIn.Month == month);
        if(!booking.Any()) return 0;

        var prices = booking.Select(b =>
            _store.RoomTypes.FirstOrDefault(rt => rt.Name == b.RoomType)?.BasePrice ?? 0);
        return prices.Average();
    }
    
    //Method To Predict Occupancy
    public double PredictOccupancy(DateOnly date)
    {
        var totalRooms = 10; //Temporary Hardcoded
        var count = _store.Bookings.Count(b =>
            date >= b.CheckIn && date <= b.CheckOut);
        return (double)count / totalRooms;
    }
}