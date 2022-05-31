using System.Reflection;
using MBMailCore.Core;
using MBMailCore.Extensions;

namespace MBMailCore.XUnitTests;

public class MailExtensionsTest
{
    [ Fact ]
    public void SetUsername_ShouldReturn_TheGivenUsername()
    {
        // Arrange
        var host     = "smtp.outlook.com";
        var port     = 2525;
        var mail     = new Mail( host , port );
        var username = "MBARK";

        // Act
        mail.SetUsername( username );
        var actualPrivateUsername = typeof(Mail).GetProperty("Username", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mail, null).ToString();

        // Assert
        Assert.Equal( actualPrivateUsername , username );
    }  
    
    [ Fact ]
    public void SetPassword_ShouldReturn_TheGivenPassword()
    {
        // Arrange
        var host     = "smtp.outlook.com";
        var port     = 2525;
        var mail     = new Mail( host , port );
        var password = "123456";

        // Act
        mail.SetPassword( password );
        var actualPrivatePassword = typeof(Mail).GetProperty("Password", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mail, null).ToString();

        // Assert
        Assert.Equal( actualPrivatePassword , password );
    }
}