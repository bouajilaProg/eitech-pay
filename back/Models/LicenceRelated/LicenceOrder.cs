namespace Back.Models.LicenceRelated;

public enum LicenceOrderStatus
{
    Active,
    Expired,
    Canceled
}

public class LicenceOrder
{
    public int LicenceOrderId { get; set; }
    public int UserId { get; set; }
    public string LicenceId { get; set; }
    public string PrivateKey { get; set; } = null!;
    public DateTime PurchaseDate { get; set; }
    public LicenceOrderStatus Status { get; set; }
    public string Reseller{ get; set; } = null!;
    public bool IsArchived { get; set; }
}
