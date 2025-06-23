namespace Back.Modules.SubscriptionModule.Dtos
{
    public class PaymentRequestDto
    {
        public decimal Amount { get; set; }
        public string Description { get; set; } = null!;
        public string[] AcceptedPaymentMethods { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string ReferenceId { get; set; } = null!;
    }
}
