
using Back.Models.SubscriptionRelated;
using System.Collections.Generic;

namespace Back.Modules.SubscriptionModule.Dtos
{
    public class SubscriptionDetailsDto
    {
        public string SubscriptionId { get; set; }
        public string SubscriptionName { get; set; } = null!;
        public string Description { get; set; } = null!;

        // Use the premade SubscriptionTier class here
        public List<SubscriptionTier> Tiers { get; set; } = new();
    }
}
