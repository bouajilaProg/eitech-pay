
using System;
using Dapper;
using System.Data;

using System.Collections.Generic;
using System.Threading.Tasks;

using Back.Models.SubscriptionRelated;
using Back.Models.General;

using Back.Modules.SubscriptionModule.Dtos;
using Back.Modules.SubscriptionModule.Services;

namespace Back.Modules.SubscriptionModule.Services
{
    public class SubscriptionPublicService : ISubscriptionPublicService
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly ITiersService _tiersService;
        private readonly IDbConnection _db;

        public SubscriptionPublicService(
            ISubscriptionService subscriptionService,
            ITiersService tierService,
            IDbConnection db)
        {
            _subscriptionService = subscriptionService;
            _tiersService = tierService;
            _db = db;
        }


      
                      
            public async Task<CheckSubscriptionDto?> CheckSubscriptionAsync(string tierId, string email, string tel)
            {
                // Find the user ID
                const string findUserQuery = @"
                    SELECT user_id FROM users
                    WHERE email = @Email OR phone = @Tel;";

                var user = await _db.QueryFirstOrDefaultAsync<User>(findUserQuery, new { Email = email, Tel = tel });
                if (user == null)
                    return null;

                Console.WriteLine("User found ");

                // Find the subscription order
                const string findOrderQuery = @"
                    SELECT * FROM subscription_order
                    WHERE user_id = @UserId AND subscription_tier_id = @TierId AND status = 'Active' AND is_archived = false;";

                var order = await _db.QueryFirstOrDefaultAsync<SubscriptionOrder>(findOrderQuery, new { UserId = user.UserId, TierId = tierId });
                if (order == null)
                    return null;

                // Find the tier and subscription
                const string findTierQuery = @"SELECT * FROM subscription_tier WHERE tier_id = @TierId;";
                var tier = await _db.QueryFirstOrDefaultAsync<SubscriptionTier>(findTierQuery, new { TierId = tierId });

                const string findSubQuery = @"SELECT * FROM subscription WHERE subscription_id = @SubscriptionId;";
                var sub = await _db.QueryFirstOrDefaultAsync<Subscription>(findSubQuery, new { SubscriptionId = order.SubscriptionId });

                if (tier == null || sub == null)
                    return null;

                // Build and return the DTO
                return new CheckSubscriptionDto
                {
                    IsActive = order.Status == SubscriptionOrderStatus.Active,
                    IsCanceled = order.Status == SubscriptionOrderStatus.Canceled,

                    SubscriptionId = sub.SubscriptionId,
                    SubscriptionName = sub.SubscriptionName,
                    SubscriptionDescription = sub.description,

                    TierName = tier.TierName,
                    TierDescription = tier.Description,
                    Duration = tier.Duration,
                    GracePeriod = tier.GracePeriod,
                    Price = tier.Price
                };
            }

            public async Task<SubscriptionDetailsDto?> GetSubscriptionDetailsAsync(string subscriptionId)
            {
            var subscription = await _subscriptionService.GetByIdAsync(subscriptionId);
            if (subscription == null || subscription.IsArchived)
                return null;

            var tiers = await _tiersService.GetSubscriptionByID(subscriptionId);

            Console.WriteLine($"Tiers count for subscription {subscriptionId}: {tiers.Count()}");

           
          return new SubscriptionDetailsDto
          {
              SubscriptionId = subscription.SubscriptionId,
              SubscriptionName = subscription.SubscriptionName,
              Description = subscription.description,
              Tiers = tiers.ToList()
          };
        }
    }
}
