namespace Back.Models.LicenceRelated;

public class LicenceActivation
{
    public int Id { get; set; }
    public int LicenceOrderId { get; set; }
    public int UserId { get; set; }
    public DateTime ActivationDate { get; set; }
    public bool IsArchived { get; set; }
}
