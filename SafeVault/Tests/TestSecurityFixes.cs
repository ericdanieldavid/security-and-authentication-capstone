using NUnit.Framework;

[TestFixture]
public class TestSecurityFixes
{
    [Test]
    public void TestSQLInjectionBlocked()
    {
        string maliciousInput = "'; DROP TABLE Users; --";
        var user = repo.GetUserByUsername(maliciousInput);
        Assert.IsNull(user, "SQL injection attempt should not return data.");
    }

    [Test]
    public void TestXSSBlocked()
    {
        string maliciousInput = "<script>alert('XSS');</script>";
        string safeOutput = SafeOutput(maliciousInput);
        Assert.IsFalse(safeOutput.Contains("<script>"), "XSS attempt should be encoded.");
        Assert.IsTrue(safeOutput.Contains("&lt;script&gt;"), "Malicious script should be safely encoded.");
    }
}
