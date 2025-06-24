namespace Back.Models.SubscriptionRelated;

public class Subscription
{
    public string SubscriptionId { get; set; } = null!;
    public string SubscriptionName { get; set; } = null!;
    public string description { get; set; } = null!;
    public bool IsArchived { get; set; }
}  
