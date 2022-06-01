using System.Reflection;
using FluentAssertions;
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

    [ Fact ]
    public void From_ShouldReturn_TheGivenEmail()
    {
        // Arrange
        var host        = "smtp.outlook.com";
        var port        = 2525;
        var mail        = new Mail(host, port);
        var senderEmail = "mbark@outlook.com";

        // Act
        mail.From( senderEmail );
        var currentSenderEmail = mail.MailMessage.From.Address;

        // Assert
        currentSenderEmail.Should().Be( senderEmail );
    }

    [ Fact ]
    public void To_ShouldReturn_TheGivenEmail()
    {
        // Arrange
        var host          = "smtp.outlook.com";
        var port          = 2525;
        var mail          = new Mail(host, port);
        var receiverEmail = "mbark@outlook.com";

        // Act
        mail.To( receiverEmail );
        var currentReceiverEmail = mail.MailMessage.To.First().Address;

        // Assert
        currentReceiverEmail.Should().Be( receiverEmail );
    }

    [Fact]
    public void To_ShouldReturn_TheGivenEmails()
    {
        // Arrange
        var host            = "smtp.outlook.com";
        var port            = 2525;
        var mail            = new Mail(host, port);
        var receiversEmails = new List<string>()
                              {
                                  "mbark@outlook.com" ,
                                  "mbark1@outlook.com" ,
                                  "mbark2@outlook.com" ,
                                  "mbark3@outlook.com" ,
                                  "mbark4@outlook.com" ,
                              };

        // Act
        mail.To( receiversEmails );
        var currentReceiversEmails    = mail.MailMessage.To.Select( x => x.Address ).ToList();
        var currentFirstReceiverEmail = mail.MailMessage.To.First().Address;

        // Assert
        currentReceiversEmails.Should().BeEquivalentTo( receiversEmails );
        currentFirstReceiverEmail.Should().Be( receiversEmails.First() );
    }
}