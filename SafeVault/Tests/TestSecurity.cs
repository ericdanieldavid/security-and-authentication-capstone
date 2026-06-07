using NUnit.Framework;

[TestFixture]
public class TestSecurity
{
    [Test]
    public void TestForSQLInjection()
    {
        string maliciousInput = "'; DROP TABLE Users; --";
        string sanitized = InputSanitizer.SanitizeUsername(maliciousInput);
        Assert.IsFalse(sanitized.Contains("DROP"), "SQL injection attempt should be neutralized.");
    }

    [Test]
    public void TestForXSS()
    {
        string maliciousInput = "<script>alert('XSS');</script>";
        string sanitized = InputSanitizer.SanitizeUsername(maliciousInput);
        Assert.IsFalse(sanitized.Contains("<script>"), "XSS attempt should be removed.");
    }

    [Test]
    public void TestValidEmail()
    {
        string email = "user@example.com";
        string sanitized = InputSanitizer.SanitizeEmail(email);
        Assert.AreEqual(email, sanitized, "Valid email should pass unchanged.");
    }

    [Test]
    public void TestInvalidEmail()
    {
        string email = "bad-email@@example";
        string sanitized = InputSanitizer.SanitizeEmail(email);
        Assert.AreEqual(string.Empty, sanitized, "Invalid email should be rejected.");
    }
}
