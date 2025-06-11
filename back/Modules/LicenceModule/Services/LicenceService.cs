using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Back.Models.LicenceRelated;
using Dapper;

namespace Back.Modules.LicenceModule.Services
{
    public class LicenceService : ILicenceService
    {
        private readonly IDbConnection _db;

        public LicenceService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<Licence?> GetByIdAsync(int licenceId)
        {

            const string query = @"
                SELECT * FROM licences 
                WHERE licence_id = @LicenceId AND is_archived = false;
            ";

            return await _db.QueryFirstOrDefaultAsync<Licence>(query, new { LicenceId = licenceId });
        }

        public async Task<IEnumerable<Licence>> GetAllAsync()
        {
            const string query = @"
                SELECT * FROM licences 
                WHERE is_archived = false;
            ";

            return await _db.QueryAsync<Licence>(query);
        }

        public async Task<int> CreateAsync(Licence licence)
        {
            const string query = @"
                INSERT INTO licences (licence_id,product_id, max_devices, duration, grace_period, public_key, price, is_archived)
                VALUES (@LicenceId,@ProductId, @MaxDevices, @Duration, @GracePeriod, @PublicKey, @Price, false);
                SELECT LAST_INSERT_ID();
            ";

            return await _db.ExecuteScalarAsync<int>(query, licence);
        }

        public async Task<bool> UpdateAsync(Licence licence)
        {
            const string query = @"
                UPDATE licences
                SET product_id = @ProductId,
                    max_devices = @MaxDevices,
                    duration = @Duration,
                    grace_period = @GracePeriod,
                    public_key = @PublicKey,
                    price = @Price
                WHERE licence_id = @LicenceId AND is_archived = false;
            ";

            var affectedRows = await _db.ExecuteAsync(query, licence);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int licenceId)
        {
            const string query = @"
                UPDATE licences
                SET is_archived = true
                WHERE licence_id = @LicenceId AND is_archived = false;
            ";

            var affectedRows = await _db.ExecuteAsync(query, new { LicenceId = licenceId });
            return affectedRows > 0;
        }
    }
}
