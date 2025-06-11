using System.Collections.Generic;
using System.Threading.Tasks;
using Back.Models.General;

namespace Back.Modules.GeneralServices
{
    public interface IProductService
    {
        // Get a product by its ID, only if not archived
        Task<Product?> GetByIdAsync(int productId);

        // Get all products that are not archived
        Task<IEnumerable<Product>> GetAllAsync();

        // Get all products of a specific type (Licence or Subscription), not archived
        Task<IEnumerable<Product>> GetByTypeAsync(ProductType productType);

        // Create a new product
        Task<int> CreateAsync(Product product);

        // Update an existing product by ID, only if not archived
        Task<bool> UpdateAsync(Product product);

        // Soft delete a product by ID (set IsArchived = true)
        Task<bool> DeleteAsync(int productId);
    }
}
