
namespace Back.Modules.LicenceModule.Dtos;

public class CreateLicenceDto
{
    public string LicenceId { get; set; }
    public string ProductName { get; set; } = null!;
    public string ProductDescription { get; set; } = null!;

    
    public int MaxDevices { get; set; }
    public int Duration { get; set; }
    public int GracePeriod { get; set; }
    public decimal Price { get; set; }
}
