// Dtos/CreateLicenceOrderDto.cs
namespace Back.Modules.LicenceModule.Dtos
{
    public class CreateLicenceOrderDto
    {
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string ReferenceId { get; set; } = null!;
        public decimal AmountPaid { get; set; }
        public string PaymentRef { get; set; } = null!;
        public int UserId { get; set; }
        public string LicenceId { get; set; } = null!;
    }
}
