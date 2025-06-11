using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Back.Models.SubscriptionRelated;
using Dapper;

namespace Back.Modules.SubscriptionModule.Services
{
    public class TiersService : ITiersService
    {
        private readonly IDbConnection _db;

        public TiersService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<SubscriptionTier?> GetByIdAsync(int tierId)
        {
            const string query = @"
                SELECT * FROM subscription_tiers
                WHERE tier_id = @TierId AND is_archived = false;
            ";

            return await _db.QueryFirstOrDefaultAsync<SubscriptionTier>(query, new { TierId = tierId });
        }

        public async Task<IEnumerable<SubscriptionTier>> GetAllAsync()
        {
            const string query = @"
                SELECT * FROM subscription_tiers
                WHERE is_archived = false;
            ";

            return await _db.QueryAsync<SubscriptionTier>(query);
        }

        public async Task<IEnumerable<SubscriptionTier>> GetSubscriptionByID(int productId)
        {
            const string query = @"
                SELECT * FROM subscription_tiers
                WHERE product_id = @ProductId AND is_archived = false;
            ";

            return await _db.QueryAsync<SubscriptionTier>(query, new { ProductId = productId });
        }

        public async Task<int> CreateAsync(SubscriptionTier tier)
        {
            const string query = @"
                INSERT INTO subscription_tiers (product_id, tier_name, duration, grace_period, price, is_archived)
                VALUES (@ProductId, @TierName, @Duration, @GracePeriod, @Price, false);
                SELECT LAST_INSERT_ID();
            ";

            return await _db.ExecuteScalarAsync<int>(query, tier);
        }

        public async Task<bool> UpdateAsync(SubscriptionTier tier)
        {
            const string query = @"
                UPDATE subscription_tiers
                SET product_id = @ProductId,
                    tier_name = @TierName,
                    duration = @Duration,
                    grace_period = @GracePeriod,
                    price = @Price
                WHERE tier_id = @TierId AND is_archived = false;
            ";

            var affectedRows = await _db.ExecuteAsync(query, tier);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int tierId)
        {
            const string query = @"
                UPDATE subscription_tiers
                SET is_archived = true
                WHERE tier_id = @TierId AND is_archived = false;
            ";

            var affectedRows = await _db.ExecuteAsync(query, new { TierId = tierId });
            return affectedRows > 0;
        }
    }
}
