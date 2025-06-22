namespace Back.Models.LicenceRelated;

public class LicenceActivation
{
    public int ActivationId { get; set; }
    public int LicenceOrderId { get; set; }
    public string DevicePrint { get; set; } = null!;
    public int UserId { get; set; }
    public DateTime ActivationDate { get; set; }
    public bool IsArchived { get; set; }
}
