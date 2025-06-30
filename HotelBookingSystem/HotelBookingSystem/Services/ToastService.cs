namespace HotelBookingSystem.Services;

public enum ToastType
{
    Success,
    Info,
    Warning,
    Error
}

public class ToastService
{
    public event Action<string, ToastType>? OnShow;

    public void ShowToast(string message, ToastType type = ToastType.Success)
    {
        OnShow?.Invoke(message, type);
    }
}