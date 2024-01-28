namespace Frank.Security.HaveIBeenPwned;

public class PasswordDetails
{
    public uint TimesPwned { get; set; } = 0;
    public string Sha1Prefix { get; set; }
    public string Sha2Suffix { get; set; }
    public string Sha1Hash { get; set; }
}