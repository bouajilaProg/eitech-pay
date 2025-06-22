namespace Back.Models.General;

public class Admin
{
    public int AdminId { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ApiKey { get; set; } = null!;
    public bool isArchived { get; set; } = false;
}
