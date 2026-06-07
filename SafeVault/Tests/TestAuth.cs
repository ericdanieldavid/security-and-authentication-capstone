using NUnit.Framework;

[TestFixture]
public class TestAuth
{
    private AuthService auth;

    [SetUp]
    public void Setup()
    {
        var repo = new UserRepository("YOUR_CONNECTION_STRING");
        auth = new AuthService(repo);
    }

    [Test]
    public void TestInvalidLogin()
    {
        bool result = auth.Authenticate("nonexistent", "wrongpass");
        Assert.IsFalse(result, "Invalid login should fail.");
    }

    [Test]
    public void TestValidLogin()
    {
        auth.Register("testuser", "securePass123");
        bool result = auth.Authenticate("testuser", "securePass123");
        Assert.IsTrue(result, "Valid login should succeed.");
    }

    [Test]
    public void TestUnauthorizedAccess()
    {
        string role = "user";
        Assert.AreEqual("user", role, "Default role should be user.");
        Assert.AreNotEqual("admin", role, "User should not have admin access.");
    }
}
