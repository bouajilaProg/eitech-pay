using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

using Back.Modules.SubscriptionModule.Dtos;

using Back.Models.SubscriptionRelated;
using Back.Models.General;


namespace Back.Modules.SubscriptionModule.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IDbConnection _db;

        public SubscriptionService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<Subscription?> GetByIdAsync(string subscriptionId)
        {
            const string query = @"
                SELECT
                    subscription_id AS SubscriptionId,
                    subscription_name AS SubscriptionName,
                    description,
                    is_archived AS IsArchived
                FROM subscription
                WHERE subscription_id = @SubscriptionId AND is_archived = false;
            ";

            return await _db.QueryFirstOrDefaultAsync<Subscription>(query, new
            {
                SubscriptionId = subscriptionId
            });
        }

        public async Task<IEnumerable<Subscription>> GetAllAsync()
        {
            const string query = @"
                SELECT
                    subscription_id AS SubscriptionId,
                    subscription_name AS SubscriptionName,
                    description,
                    is_archived AS IsArchived
                FROM subscription
                WHERE is_archived = false;
            ";

            return await _db.QueryAsync<Subscription>(query);
        }

        public async Task<string> CreateAsync(Subscription subscription)
        {
            const string query = @"
                INSERT INTO subscription (subscription_id,subscription_name, description, is_archived)
                VALUES (@SubscriptionId, @SubscriptionName,@description, @IsArchived)
                RETURNING subscription_id;
            ";

            return await _db.ExecuteScalarAsync<string>(query, subscription);
        }

        public async Task<bool> UpdateAsync(Subscription subscription)
        {
            const string query = @"
                UPDATE subscription
                SET
                    description = @description
                WHERE subscription_id = @SubscriptionId AND is_archived = false;
            ";

            var affected = await _db.ExecuteAsync(query, subscription);
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(string subscriptionId)
        {
            const string query = @"
                UPDATE subscription
                SET is_archived = true
                WHERE subscription_id = @SubscriptionId AND is_archived = false;
            ";

            var affected = await _db.ExecuteAsync(query, new
            {
                SubscriptionId = subscriptionId
            });

            return affected > 0;
        }

        // Get subscription details including tiers
        public async Task<SubscriptionDetailsDto?> GetSubscriptionDetailsAsync(string subscriptionId)
        {
            // Get subscription info
            const string subQuery = @"
                SELECT subscription_id AS SubscriptionId, subscription_name AS SubscriptionName, description
                FROM subscription
                WHERE subscription_id = @SubscriptionId AND is_archived = false;
            ";
            var subscription = await _db.QueryFirstOrDefaultAsync<SubscriptionDetailsDto>(subQuery, new { SubscriptionId = subscriptionId });
            if (subscription == null) return null;

            // Get tiers
            const string tiersQuery = @"
                SELECT * FROM subscription_tier WHERE product_id = @SubscriptionId AND is_archived = false;
            ";
            var tiers = (await _db.QueryAsync<SubscriptionTier>(tiersQuery, new { SubscriptionId = subscriptionId })).AsList();
            subscription.Tiers = tiers;
            return subscription;
        }

        // Get monthly revenue for all subscriptions (sum grouped by month)
        public async Task<MonthlyRevenue> GetMonthlyRevenueAsync()
        {
            const string query = @"
                SELECT Month, SUM(Revenue) AS Revenue
                FROM monthly_revenue
                WHERE Type = 'Subscription'
                GROUP BY Month
                ORDER BY Month DESC
                LIMIT 1;
            ";
            // Only returning the latest month as an example
            return await _db.QueryFirstOrDefaultAsync<MonthlyRevenue>(query);
        }

        // Get monthly revenue for a specific subscription
        public async Task<MonthlyRevenue> GetMonthlyRevenueAsync(string subscriptionId)
        {
            const string query = @"
                SELECT * FROM monthly_revenue
                WHERE product_id = @SubscriptionId AND Type = 'Subscription'
                ORDER BY Month DESC
                LIMIT 1;
            ";
            return await _db.QueryFirstOrDefaultAsync<MonthlyRevenue>(query, new { SubscriptionId = subscriptionId });
        }

        // Insert monthly revenue for a specific subscription
        public async Task<bool> InsertMonthlyRevenueAsync(MonthlyRevenue monthlyRevenue)
        {
            const string query = @"
                INSERT INTO monthly_revenue (Month, product_id, Type, Revenue)
                VALUES (@Month, @productId, @Type, @Revenue);
            ";
            var result = await _db.ExecuteAsync(query, monthlyRevenue);
            return result > 0;
        }

        // Get stats (total sales, revenue, users)
        public async Task<Stats> GetStatsAsync()
        {
            const string query = @"SELECT * FROM stats LIMIT 1;";
            return await _db.QueryFirstOrDefaultAsync<Stats>(query);
        }
    }
}
