using System.Text.RegularExpressions;

public static class InputSanitizer
{
    public static string SanitizeUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username)) return string.Empty;

        string sanitized = Regex.Replace(username, @"[<>;'""--]", string.Empty);

        if (sanitized.Length > 100)
            sanitized = sanitized.Substring(0, 100);

        return sanitized;
    }

    public static string SanitizeEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return string.Empty;

        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            return string.Empty;

        return email.Trim();
    }
}
