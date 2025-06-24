namespace Back.Modules.LicenceModule.Dtos;

public class CreateLicenceDto
{
    public string LicenceId { get; set; }
    public string LicenceName { get; set; } = null!;
    public string Description { get; set; } = null!;

    
    public int MaxDevices { get; set; }
    public int Duration { get; set; }
    public string GracePeriod { get; set; }
    public decimal Price { get; set; }
}
