namespace Back.Models.SubscriptionRelated;

public enum SubscriptionOrderStatus
{
    Active,
    Expired,
    Canceled
}

public class SubscriptionOrder
{
    public int Id { get; set; }
    public int SubscriptionTierId { get; set; }
    public int UserId { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public SubscriptionOrderStatus Status { get; set; }
    public bool IsArchived { get; set; }
}
