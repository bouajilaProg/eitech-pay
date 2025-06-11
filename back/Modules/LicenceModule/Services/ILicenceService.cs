using System.Collections.Generic;
using System.Threading.Tasks;

using Back.Models.LicenceRelated;
using Back.Modules.LicenceModule.Dtos;


namespace Back.Modules.LicenceModule.Services
{
    public interface ILicenceService
    {
        // Get a license by its ID, only if not archived
        Task<Licence?> GetByIdAsync(int licenceId);

        // Get all licenses that are not archived
        Task<IEnumerable<Licence>> GetAllAsync();

        // Create a new license
        Task<int> CreateAsync(CreateLicenceDto createLicenceDto);

        // Update an existing license by ID, only if not archived
        Task<bool> UpdateAsync(Licence licence);

        // Soft delete a license by ID (set is_archived = true)
        Task<bool> DeleteAsync(int licenceId);


    }  
}
