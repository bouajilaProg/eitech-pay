using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Back.Models.SubscriptionRelated;
using Dapper;

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
                    description,
                    is_archived AS IsArchived
                FROM subscriptions
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
                    description,
                    is_archived AS IsArchived
                FROM subscriptions
                WHERE is_archived = false;
            ";

            return await _db.QueryAsync<Subscription>(query);
        }

        public async Task<string> CreateAsync(Subscription subscription)
        {
            const string query = @"
                INSERT INTO subscriptions (subscription_id, description, is_archived)
                VALUES (@SubscriptionId, @description, @IsArchived)
                RETURNING subscription_id;
            ";

            return await _db.ExecuteScalarAsync<string>(query, subscription);
        }

        public async Task<bool> UpdateAsync(Subscription subscription)
        {
            const string query = @"
                UPDATE subscriptions
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
                UPDATE subscriptions
                SET is_archived = true
                WHERE subscription_id = @SubscriptionId AND is_archived = false;
            ";

            var affected = await _db.ExecuteAsync(query, new
            {
                SubscriptionId = subscriptionId
            });

            return affected > 0;
        }
    }
}
