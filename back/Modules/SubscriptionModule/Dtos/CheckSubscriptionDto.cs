

using Back.Models.SubscriptionRelated;

namespace Back.Modules.SubscriptionModule.Dtos
{
    public class CheckSubscriptionDto
    {
      public bool IsActive { get; set; }
      public bool IsCanceled { get; set; }

      public string SubscriptionId { get; set; } = null!;
      public string SubscriptionName { get; set; } = null!;
      public string SubscriptionDescription { get; set; } = null!;

      public string TierName { get; set; } = null!;
      public string TierDescription { get; set; } = null!;
      public int Duration { get; set; }
      public string GracePeriod { get; set; }
      public decimal Price { get; set; }
    } 
}
