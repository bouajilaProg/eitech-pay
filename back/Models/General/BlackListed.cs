namespace Back.Models.General;

public enum BlackListType
{
    IP,
    User,
    Device
}

public class BlackListed
{
    public int Id { get; set; }
    public string Ip { get; set; } = null!;
    public int userId { get; set; }
    public BlackListType Type { get; set; }
    public DateTime BlockedDate { get; set; }
    public DateTime? RecoveryDate { get; set; }
}
