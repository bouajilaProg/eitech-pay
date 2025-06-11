namespace Back.Models.General;

public enum ProductType
{
    Licence,
    Subscription
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ProductType { get; set; }
    public bool IsArchived { get; set; }
}
