namespace Back.Models.General;

public enum RevenueType
{
    Subscription,
    Licence
}

public class MonthlyRevenue
{
    public DateTime Month { get; set; }
    public string productId { get; set; } = null!;
    public RevenueType Type { get; set; }
    public decimal Revenue { get; set; }
}
