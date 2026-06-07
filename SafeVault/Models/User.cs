public class User
{
    public int UserID { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; } // e.g., "admin", "user"
}
