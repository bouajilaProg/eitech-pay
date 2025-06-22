namespace Back.Models.SubscriptionRelated;

public enum SubscriptionOrderStatus
{
    Active,
    Expired,
    Canceled
}

public class SubscriptionOrder
{
    public int OrderId { get; set; }
    public string SubscriptionId { get; set; }
    public string SubscriptionTierId { get; set; }

    public int UserId { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public SubscriptionOrderStatus Status { get; set; }
    public string Reseller { get; set; } = null!;
    public bool IsArchived { get; set; }
}
