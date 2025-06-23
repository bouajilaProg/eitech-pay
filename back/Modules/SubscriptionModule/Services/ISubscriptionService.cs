
using System.Collections.Generic;
using System.Threading.Tasks;

using Back.Models.SubscriptionRelated;
using Back.Models.General;

using Back.Modules.SubscriptionModule.Dtos;


namespace Back.Modules.SubscriptionModule.Services
{
    public interface ISubscriptionService
    {
        // Get a subscription product by its ID, only if not archived
        Task<Subscription?> GetByIdAsync(string productId);

        // Get all subscription products that are not archived
        Task<IEnumerable<Subscription>> GetAllAsync();

        // Create a new subscription product
        Task<string> CreateAsync(Subscription subscription);

        // Update an existing subscription product by ID, only if not archived
        Task<bool> UpdateAsync(Subscription subscription);

        // Soft delete a subscription product by ID (set IsArchived = true)
        Task<bool> DeleteAsync(string SubscriptionId);

        // get subscription Details
        Task<SubscriptionDetailsDto?> GetSubscriptionDetailsAsync(string subscriptionId);

        // get monthly Revenue for alll
        Task<MonthlyRevenue> GetMonthlyRevenueAsync();

        // get monthly Revenue for a specific subscription
        Task<MonthlyRevenue> GetMonthlyRevenueAsync(string subscriptionId);

        // insert monthly revenue for a specific subscription
        Task<bool> InsertMonthlyRevenueAsync(MonthlyRevenue monthlyRevenue);



        // get stats
        Task<Stats> GetStatsAsync();


    }
}
