namespace Back.Models.SubscriptionRelated;

public class SubscriptionTier
{
    public string TierId { get; set; }
    public string SubscriptionId { get; set; }
    public string TierName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Duration { get; set; }
    public int GracePeriod { get; set; }
    public decimal Price { get; set; }
    public bool IsArchived { get; set; }
} 
