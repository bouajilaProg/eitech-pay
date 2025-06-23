namespace Back.Modules.LicenceModule.Dtos
{
    public class ActivateLicenceRequestDto
    {
        public string Email { get; set; } = null!;
        public string LicenceKey { get; set; } = null!;
        public string DeviceFingerprint { get; set; } = null!;
    }
}
