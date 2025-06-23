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

        public async Task<SubscriptionTier?> GetByIdAsync(string tierId)
        {
            const string query = @"
                SELECT * FROM subscription_tier
                WHERE tier_id = @TierId AND is_archived = false;
            ";

            return await _db.QueryFirstOrDefaultAsync<SubscriptionTier>(query, new { TierId = tierId });
        }

        public async Task<IEnumerable<SubscriptionTier>> GetAllAsync()
        {
            const string query = @"
                SELECT * FROM subscription_tier
                WHERE is_archived = false;
            ";

            return await _db.QueryAsync<SubscriptionTier>(query);
        }

        public async Task<IEnumerable<SubscriptionTier>> GetSubscriptionByID(string subscriptionId)
        {
            const string query = @"
                SELECT * FROM subscription_tier
                WHERE subscription_id = @SubscriptionId AND is_archived = false;
            ";

            return await _db.QueryAsync<SubscriptionTier>(query, new { subscriptionId = subscriptionId });
        }

        public async Task<string> CreateAsync(SubscriptionTier tier)
        {
            const string query = @"
                INSERT INTO subscription_tier (tier_id,subscription_id, tier_name,description, duration, grace_period, price, is_archived)
                VALUES (@TierId,@SubscriptionId, @TierName,@Description ,@Duration, @GracePeriod, @Price, false);
                SELECT LAST_INSERT_ID();
            ";

            return await _db.ExecuteScalarAsync<string>(query, tier);
        }

        public async Task<bool> UpdateAsync(SubscriptionTier tier)
        {
            const string query = @"
                UPDATE subscription_tier
                SET tier_id = @TierId,
                subscription_id = @SubscriptionId,
                tier_name = @TierName,
                description = @Description,
                duration = @Duration,
                grace_period = @GracePeriod,
                price = @Price
                WHERE tier_id = @TierId AND is_archived = false;
            ";

            var affectedRows = await _db.ExecuteAsync(query, tier);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(string tierId)
        {
            const string query = @"
                UPDATE subscription_tier
                SET is_archived = true
                WHERE tier_id = @TierId AND is_archived = false;
            ";

            var affectedRows = await _db.ExecuteAsync(query, new { TierId = tierId });
            return affectedRows > 0;
        }
    }
}
