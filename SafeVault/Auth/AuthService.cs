using BCrypt.Net;

public class AuthService
{
    private readonly UserRepository _repo;

    public AuthService(UserRepository repo)
    {
        _repo = repo;
    }

    public bool Register(string username, string password, string role = "user")
    {
        string hash = BCrypt.Net.BCrypt.HashPassword(password);
        return _repo.AddUserWithPassword(username, hash, role);
    }

    public bool Authenticate(string username, string password)
    {
        var user = _repo.GetUserByUsername(username);
        if (user == null) return false;

        return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
    }
}
