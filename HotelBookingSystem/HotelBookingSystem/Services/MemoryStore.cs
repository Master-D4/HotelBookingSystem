namespace HotelBookingSystem.Services;

using HotelBookingSystem.Models;

public class MemoryStore
{
    public List<RoomType> RoomTypes { get; set; } = new();
    public List<Booking> Bookings { get; set; } = new();

    public MemoryStore()
    {
        //Add Room Types
        RoomTypes.AddRange(new[]
        {
            new RoomType { Name = "Single", BasePrice = 100 },
            new RoomType { Name = "Double", BasePrice = 150 },
            new RoomType { Name = "Suite", BasePrice = 250 },
        });
        
        //Add Bookings
        Bookings.Add(new Booking
        {
            CheckIn = DateOnly.FromDateTime(DateTime.Today),
            CheckOut = DateOnly.FromDateTime(DateTime.Today.AddDays(2)),
            RoomType = "Double",
            SpecialRequest = "Late check-in",
            IsRecurring = true,
        });
    }
}