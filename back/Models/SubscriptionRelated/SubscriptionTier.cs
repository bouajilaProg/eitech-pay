namespace Back.Models.SubscriptionRelated;

public class SubscriptionTier
{
    public int TierId { get; set; }
    public int ProductId { get; set; }
    public string TierName { get; set; } = null!;
    public int Duration { get; set; }
    public int GracePeriod { get; set; }
    public decimal Price { get; set; }
    public bool IsArchived { get; set; }
}
