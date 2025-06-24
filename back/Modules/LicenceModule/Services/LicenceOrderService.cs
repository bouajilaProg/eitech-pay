using System.Data;
using System.Threading.Tasks;
using Back.Models.LicenceRelated;
using Dapper;

namespace Back.Modules.LicenceModule.Services
{
    public class LicenceOrderService : ILicenceOrderService
    {
        private readonly IDbConnection _db;

        public LicenceOrderService(IDbConnection db)
        {
            _db = db;
        }

        public async Task CreateAsync(LicenceOrder order)
        {
            const string query = @"
                INSERT INTO licence_orders (licence_order_id, user_id, licence_id, private_key, purchase_date, status, reseller, is_archived)
                VALUES (@LicenceOrderId, @UserId, @LicenceId, @PrivateKey, @PurchaseDate, @Status, @Reseller, @IsArchived)";

            await _db.ExecuteAsync(query, order);
        }
    }
}
