namespace Back.Models.SubscriptionRelated;

public class Subscription
{
    public string SubscriptionId { get; set; }
    public string description { get; set; } = null!;
    public bool IsArchived { get; set; }
} 
