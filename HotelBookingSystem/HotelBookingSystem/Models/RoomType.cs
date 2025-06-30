using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models;

public class RoomType
{
    [Required(ErrorMessage = "Room type name is required")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Base price is required")]
    [Range(1, 1000000, ErrorMessage = "Price must be between 1 and 1000000")]
    public decimal BasePrice { get; set; }
}