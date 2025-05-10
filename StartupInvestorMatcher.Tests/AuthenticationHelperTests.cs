namespace StartupInvestorMatcher.Tests;

[TestClass]
public class UnitTest1
{
    [TestClass]
public class AuthenticationHelperTest {
   [TestMethod]
   public void EncryptTest() {
      // Arrange 
      string username = "john.doe";
      string password = "VerySecret!";

      var header = StartupInvestorMatcher.API.Middleware.AuthenticationHelper.Encrypt(username, password);

      Assert.AreEqual("Basic am9obi5kb2U6VmVyeVNlY3JldCE=", header);

   }

   [TestMethod]
   public void DecryptTest() {
      // Arrange
      string header = "Basic am9obi5kb2U6VmVyeVNlY3JldCE=";

    StartupInvestorMatcher.API.Middleware.AuthenticationHelper.Decrypt(header, out string username, out string password);

    Assert.AreEqual("john.doe", username);
    Assert.AreEqual("VerySecret!", password);


}
}
}
