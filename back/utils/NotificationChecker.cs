using System;
using System.Threading.Tasks;
using System.Linq;
using Dapper;
using System.Data;
using Back.Models.LicenceRelated;
using Back.Models.SubscriptionRelated;
using Back.Modules.Notification.Services;

namespace Back.Utils
{
    public static class NotificationChecker
    {
        private static readonly TimeSpan ReminderThreshold = TimeSpan.FromDays(3);

        public static async Task CheckAndNotifyAsync(IDbConnection db, INotificationService notifier)
        {
            await CheckExpirationsAsync<LicenceOrder>(db, notifier, "LicenceOrder");
            await CheckExpirationsAsync<SubscriptionOrder>(db, notifier, "SubscriptionOrder");
        }

        private static async Task CheckExpirationsAsync<T>(
            IDbConnection db,
            INotificationService notifier,
            string tableName
        ) where T : class, IExpirableOrder
        {
            var now = DateTime.UtcNow;
            var maxDate = now.Add(ReminderThreshold);

            var orders = await db.QueryAsync<T>($"SELECT * FROM {tableName} WHERE IsArchived = 0");

            foreach (var order in orders)
            {
                if (order.EndDate == null) continue;

                var expiresAt = order.EndDate.Value;

                if (expiresAt <= now)
                {
                    await SendExpirationEmailAsync(notifier, order, expiresAt);
                }
                else if (expiresAt <= maxDate)
                {
                    await SendReminderEmailAsync(notifier, order, expiresAt, (expiresAt - now).Days);
                }
            }
        }

        private static Task SendExpirationEmailAsync(INotificationService notifier, IExpirableOrder order, DateTime expiresAt)
        {
            return notifier.SendNotificationAsync(
                NotificationType.ProductExpiration,
                order.Email,
                new
                {
                    User = order.UserName,
                    ProductName = order.ProductName,
                    EndDate = expiresAt.ToString("MMMM dd, yyyy"),
                    Email = order.Email
                }
            );
        }

        private static Task SendReminderEmailAsync(INotificationService notifier, IExpirableOrder order, DateTime expiresAt, int daysLeft)
        {
            return notifier.SendNotificationAsync(
                NotificationType.PaymentReminder,
                order.Email,
                new
                {
                    User = order.UserName,
                    ProductName = order.ProductName,
                    DaysLeft = daysLeft,
                    EndDate = expiresAt.ToString("MMMM dd, yyyy"),
                    Email = order.Email
                }
            );
        }
    }

    public interface IExpirableOrder
    {
        public string UserName { get; }
        public string ProductName { get; }
        public DateTime? EndDate { get; }
        public string Email { get; }
    }
}
