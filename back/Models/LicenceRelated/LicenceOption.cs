namespace Back.Models.LicenceRelated;

public class LicenceOption
{
    public int OptionId { get; set; }
    public int LicenceId { get; set; }
    public string OptionName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public bool IsArchived { get; set; }
}
