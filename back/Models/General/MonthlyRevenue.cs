namespace Back.Models.General;

public enum RevenueType
{
    Subscription,
    Licence
}

public class MonthlyRevenue
{
    public int Month { get; set; }
    public int Year { get; set; }
    public string ProductId { get; set; }
    public decimal Revenue { get; set; }
}
