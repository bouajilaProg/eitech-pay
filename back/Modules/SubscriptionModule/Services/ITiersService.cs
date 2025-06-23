
using System.Collections.Generic;
using System.Threading.Tasks;
using Back.Models.SubscriptionRelated;

namespace Back.Modules.SubscriptionModule.Services
{
    public interface ITiersService 
    {
        // Get a subscription tier by its ID, only if not archived
        Task<SubscriptionTier?> GetByIdAsync(string tierId);

        // Get all subscription tiers that are not archived
        Task<IEnumerable<SubscriptionTier>> GetAllAsync();

        // Get all subscription tiers for a specific product, not archived
        Task<IEnumerable<SubscriptionTier>> GetSubscriptionByID(string subscriptionId);

        // Create a new subscription tier
        Task<string> CreateAsync(SubscriptionTier tier);

        // Update an existing subscription tier by ID, only if not archived
        Task<bool> UpdateAsync(SubscriptionTier tier);

        // Soft delete a subscription tier by ID (set IsArchived = true)
        Task<bool> DeleteAsync(string tierId);
    }
}
