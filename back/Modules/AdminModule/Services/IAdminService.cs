namespace AdminModule.Services
{
    public interface IAdminService
    {
        string Login(string email, string password);
        bool CheckToken(string token);

        void ChangePassword(string oldpasswd, string newpasswd);
        void ChangePaymentDetails(string apiKey, string konnectId);

        string GenerateApiKey();
        bool ValidateKonnectId(string konnectId);
        bool ValidatePassword(string password);
        bool ValidateApiKey(string apiKey);
    }
}
