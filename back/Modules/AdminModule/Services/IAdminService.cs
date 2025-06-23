namespace Back.Modules.AdminModule.Services
{
    public interface IAdminService
    {
        string Login(string username, string password);
        public (string AdminId, string AdminName) CheckToken(string token);

        void ChangePassword(string oldpasswd, string newpasswd);
        void ChangePaymentDetails(string apiKey, string konnectId);


        public string GetApiKey(int adminId);
        string GenerateApiKey();
        bool ValidateKonnectId(string konnectId);
        bool ValidatePassword(string password);
        bool ValidateApiKey(string apiKey);
    }
}
