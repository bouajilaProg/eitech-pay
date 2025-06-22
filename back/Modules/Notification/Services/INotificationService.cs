using System.Threading.Tasks;

namespace Back.Modules.Notification.Services
{
    public interface INotificationService
    {
        Task SendNotificationAsync(NotificationType type, string toEmail, object dataModel);
    }

    public enum NotificationType
    {
        ProductExpiration,
        LicensePaymentSuccess,
        SubscriptionPaymentSuccess,
        PaymentReminder
    }
}
