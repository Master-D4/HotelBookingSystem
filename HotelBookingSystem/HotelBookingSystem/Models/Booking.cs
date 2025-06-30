using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.VisualBasic.CompilerServices;

namespace HotelBookingSystem.Models;

public class Booking
{
    public Guid bookingId { get; set; } = Guid.NewGuid();
    
    [Required]
    [DataType(DataType.Date)]
    [CustomValidation(typeof(Booking), nameof(ValidateDateNotPast))]
    public DateOnly CheckIn { get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    [CustomValidation(typeof(Booking), nameof(ValidateDateNotPast))]
    public DateOnly CheckOut { get; set; }
    
    [Required(ErrorMessage = "Room type is required")]
    public string RoomType { get; set; }
    
    public string? SpecialRequest { get; set; }
    
    [Required(ErrorMessage = "Please select whether it's recurring or not.")]
    public bool IsRecurring { get; set; }


    public static ValidationResult ValidateDateNotPast(DateOnly date, ValidationContext context)
    {
        if (date < DateOnly.FromDateTime(DateTime.Today))
        {
            return new ValidationResult("Date cannot be a past date");
        }
        return ValidationResult.Success;
    }
}