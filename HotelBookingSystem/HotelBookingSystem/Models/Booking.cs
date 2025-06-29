namespace HotelBookingSystem.Models;

public class Booking
{
    public Guid bookingId { get; set; } = Guid.NewGuid();
    public DateOnly CheckIn { get; set; }
    public DateOnly CheckOut { get; set; }
    public string RoomType { get; set; }
    public string? SpecialRequest { get; set; }
    public bool IsRecurring { get; set; }
}