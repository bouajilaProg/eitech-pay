namespace Back.Models.LicenceRelated;

public class Licence
{
    public int LicenceId { get; set; }
    public int ProductId { get; set; }
    public int MaxDevices { get; set; }
    public int Duration { get; set; }
    public int GracePeriod { get; set; }
    public string PublicKey { get; set; } = null!;
    public decimal Price { get; set; }
    public bool IsArchived { get; set; }
}
