using Back.Models.General;
using Back.Models.LicenceRelated;
using Back.Modules.LicenceModule.Dtos;
using Dapper;
using System.Data;

namespace Back.Modules.LicenceModule.Services
{
    public class LicenceService : ILicenceService
    {
        private readonly IDbConnection _db;

        public LicenceService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<Licence?> GetByIdAsync(string licenceId)
        {
            const string query = @"
                SELECT * FROM licence 
                WHERE licence_id = @LicenceId AND is_archived = false;
            ";

            return await _db.QueryFirstOrDefaultAsync<Licence>(query, new { LicenceId = licenceId });
        }

        public async Task<IEnumerable<Licence>> GetAllAsync()
        {
            const string query = @"
                SELECT * FROM licence 
                WHERE is_archived = false;
            ";

            return await _db.QueryAsync<Licence>(query);
        }

        public async Task<string> CreateAsync(CreateLicenceDto createLicenceDto)
        {
            var licence = new Licence
            {
                LicenceId = createLicenceDto.LicenceId,
                LicenceName = createLicenceDto.LicenceName,
                Description = createLicenceDto.Description,
                MaxDevices = createLicenceDto.MaxDevices,
                Duration = createLicenceDto.Duration,
                GracePeriod = createLicenceDto.GracePeriod,
                PublicKey = GenerateRandomPublicKey(),
                Price = createLicenceDto.Price,
                IsArchived = false
            };

            const string licenceInsertQuery = @"
                INSERT INTO licence 
                    (licence_id, licence_name, description, max_devices, duration, grace_period, public_key, price, is_archived)
                VALUES 
                    (@LicenceId, @licenceName, @Description, @MaxDevices, @Duration, @GracePeriod, @PublicKey, @Price, @IsArchived);
            ";

            var result = await _db.ExecuteAsync(licenceInsertQuery, licence);
            if (result == 0)
                throw new Exception("Failed to create licence");

            return licence.LicenceId;
        }

        public async Task<bool> UpdateAsync(CreateLicenceDto licence)
        {
            const string query = @"
                UPDATE licence
                SET 
                    licence_name = @LicenceName,
                    description = @Description,
                    max_devices = @MaxDevices,
                    duration = @Duration,
                    grace_period = @GracePeriod,
                    price = @Price
                WHERE 
                    licence_id = @LicenceId AND is_archived = false;
            ";

            var affectedRows = await _db.ExecuteAsync(query, licence);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(string licenceId)
        {
            const string query = @"
                UPDATE licence
                SET is_archived = true
                WHERE licence_id = @LicenceId AND is_archived = false;
            ";

            var affectedRows = await _db.ExecuteAsync(query, new { LicenceId = licenceId });
            return affectedRows > 0;
        }

        private string GenerateRandomPublicKey()
        {
            return Guid.NewGuid().ToString();
        }

        public async Task<MonthlyRevenue?> GetRevenueByIdAsync(string productId)
        {
            const string query = @"
                SELECT * FROM monthly_revenue
                WHERE product_id = @ProductId
                LIMIT 1;
            ";
            return await _db.QueryFirstOrDefaultAsync<MonthlyRevenue>(query, new { ProductId = productId });
        }

        public async Task<IEnumerable<MonthlyRevenue>> GetAllRevenuesAsync()
        {
            const string query = @"
                SELECT * FROM monthly_revenue;
            ";
            return await _db.QueryAsync<MonthlyRevenue>(query);
        }

        public async Task<string> InsertRevenueAsync(MonthlyRevenue revenue)
        {
            const string query = @"
                INSERT INTO monthly_revenue (Month, product_id, Type, Revenue)
                VALUES (@Month, @productId, @Type, @Revenue);
            ";
            var result = await _db.ExecuteAsync(query, revenue);
            if (result == 0)
                throw new Exception("Failed to insert revenue record");

            return revenue.ProductId;
        }
    }
}
