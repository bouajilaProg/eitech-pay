namespace Back.Models.LicenceRelated;

public class Licence
{
    public string LicenceId { get; set; }
    public string LicenceName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int MaxDevices { get; set; }
    public int Duration { get; set; }
    public int GracePeriod { get; set; } // Changed from string to int
    public string PublicKey { get; set; } = null!;
    public decimal Price { get; set; }
    public bool IsArchived { get; set; }
}
