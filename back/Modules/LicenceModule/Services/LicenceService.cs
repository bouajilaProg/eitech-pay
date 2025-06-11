using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;


using Back.Models.General;
using Back.Models.LicenceRelated;
using Back.Modules.GeneralServices;
using Back.Modules.LicenceModule.Dtos;


namespace Back.Modules.LicenceModule.Services
{
    public class LicenceService : ILicenceService
    {
        private readonly IDbConnection _db;
        private readonly IProductService _productService;

        public LicenceService(IDbConnection db, IProductService productService)
        {
            _db = db;
            _productService = productService;
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

      
 public async Task<int> CreateAsync(CreateLicenceDto createLicenceDto)
        {
            var productFromDto = new Product
            {
                Id = createLicenceDto.LicenceId,
                Name = createLicenceDto.ProductName,
                Description = createLicenceDto.ProductDescription,
                ProductType ="Licence",
                IsArchived = false
            };

            // Use product service to create the product instead of manual SQL insert
            var productCreatedId = await _productService.CreateAsync(productFromDto);

            if (productCreatedId == 0)
                throw new Exception("Failed to create product via ProductService");

            // Now create licence referencing the created product
            var licence = new Licence
            {
                LicenceId = createLicenceDto.LicenceId,
                ProductId = createLicenceDto.LicenceId,

                MaxDevices = createLicenceDto.MaxDevices,
                Duration = createLicenceDto.Duration,
                GracePeriod = createLicenceDto.GracePeriod,
                PublicKey = GenerateRandomPublicKey(),
                Price = createLicenceDto.Price,
                IsArchived = false
            };

            const string licenceInsertQuery = @"
                INSERT INTO licences (licence_id, product_id, max_devices, duration, grace_period, public_key, price, is_archived)
                VALUES (@LicenceId, @ProductId, @MaxDevices, @Duration, @GracePeriod, @PublicKey, @Price, @IsArchived);
            ";

            var licenceCreated = await _db.ExecuteAsync(licenceInsertQuery, licence);
            if (licenceCreated == 0)
                throw new Exception("Failed to create licence");

            return licence.LicenceId;
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

        private string GenerateRandomPublicKey()
        {
            return Guid.NewGuid().ToString();
        }

    }
}
