using System.Collections.Generic;
using System.Threading.Tasks;
using Back.Models.LicenceRelated;

namespace Back.Modules.LicenceModule.Services
{
    public interface ILicenceOptionService
    {
        // Get a licence option by its ID, only if not archived
        Task<LicenceOption?> GetByIdAsync(string optionId);

        // Get all licence options that are not archived
        Task<IEnumerable<LicenceOption>> GetAllAsync();

        // Get all licence options for a specific licence, not archived
        Task<IEnumerable<LicenceOption>> GetByLicenceIdAsync(string licenceId);

        // Create a new licence option
        Task<string> CreateAsync(LicenceOption option);

        // Update an existing licence option by ID, only if not archived
        Task<bool> UpdateAsync(LicenceOption option);

        // Soft delete a licence option by ID (set IsArchived = true)
        Task<bool> DeleteAsync(string optionId);
    }
}
