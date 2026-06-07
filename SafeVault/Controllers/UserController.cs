using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

public class UserController : Controller
{
    private readonly string connectionString = "YOUR_CONNECTION_STRING";

    [HttpPost]
    public IActionResult Submit(string username, string email)
    {
        string safeUsername = InputSanitizer.SanitizeUsername(username);
        string safeEmail = InputSanitizer.SanitizeEmail(email);

        if (string.IsNullOrEmpty(safeUsername) || string.IsNullOrEmpty(safeEmail))
        {
            return BadRequest("Invalid input.");
        }

        var repo = new UserRepository(connectionString);
        repo.AddUser(safeUsername, safeEmail);

        return Ok("User added securely.");
    }
}
