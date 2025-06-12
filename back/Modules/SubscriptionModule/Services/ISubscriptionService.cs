
using System.Collections.Generic;
using System.Threading.Tasks;
using Back.Models.General;

namespace Back.Modules.SubscriptionModule.Services
{
    public interface ISubscriptionService
    {
        // Get a subscription product by its ID, only if not archived
        Task<Product?> GetByIdAsync(string productId);

        // Get all subscription products that are not archived
        Task<IEnumerable<Product>> GetAllAsync();

        // Create a new subscription product
        Task<string> CreateAsync(Product Product);

        // Update an existing subscription product by ID, only if not archived
        Task<bool> UpdateAsync(Product product);

        // Soft delete a subscription product by ID (set IsArchived = true)
        Task<bool> DeleteAsync(string productId);
    }
}
