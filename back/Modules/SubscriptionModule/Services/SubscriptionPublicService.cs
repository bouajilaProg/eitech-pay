
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Back.Models.SubscriptionRelated;

using Back.Modules.SubscriptionModule.Dtos;
using Back.Modules.SubscriptionModule.Services;

namespace Back.Modules.SubscriptionModule.Services
{
    public class SubscriptionPublicService : ISubscriptionPublicService
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly ITiersService _tiersService;

        public SubscriptionPublicService(
            ISubscriptionService subscriptionService,
            ITiersService tierService)
        {
            _subscriptionService = subscriptionService;
            _tiersService = tierService;
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
