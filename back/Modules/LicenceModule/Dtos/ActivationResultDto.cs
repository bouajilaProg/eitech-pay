namespace Back.Modules.LicenceModule.Dtos
{
    public class ActivationResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
        public string? ActivationId { get; set; }
    }
}
