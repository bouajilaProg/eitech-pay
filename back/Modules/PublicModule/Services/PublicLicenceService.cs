using System.Data;
using System.Threading.Tasks;
using System.Linq;
using Dapper;
using Back.Models.LicenceRelated;

namespace Back.Modules.PublicModule.Services
{
    public class PublicLicenceService : IPublicLicenceService
    {
        private readonly IDbConnection _db;

        public PublicLicenceService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<LicenceDetailedDto?> GetDetailedLicenceById(int licenceId)
        {
            const string licenceQuery = @"
                SELECT * FROM licences 
                WHERE licence_id = @LicenceId AND is_archived = false;
            ";

            const string optionsQuery = @"
                SELECT * FROM licence_options 
                WHERE licence_id = @LicenceId AND is_archived = false;
            ";

            var licence = await _db.QueryFirstOrDefaultAsync<Licence>(licenceQuery, new { LicenceId = licenceId });

            if (licence == null)
                return null;

            var options = await _db.QueryAsync<LicenceOption>(optionsQuery, new { LicenceId = licenceId });

            var detailedDto = new LicenceDetailedDto
            {
                LicenceId = licence.LicenceId,
                ProductId = licence.ProductId,
                MaxDevices = licence.MaxDevices,
                Duration = licence.Duration,
                GracePeriod = licence.GracePeriod,
                PublicKey = licence.PublicKey,
                Price = licence.Price,
                IsArchived = licence.IsArchived,
                Options = options.ToList()
            };

            return detailedDto;
        }
    }
}
