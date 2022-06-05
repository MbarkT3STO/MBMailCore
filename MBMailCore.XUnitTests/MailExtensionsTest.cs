using System.Net.Mime;
using System.Reflection;
using FluentAssertions;
using MBMailCore.Core;
using MBMailCore.Exceptions;
using MBMailCore.Extensions;

namespace MBMailCore.XUnitTests;

public class MailExtensionsTest
{
    [ Fact ]
    public void SetUsername_ShouldSet_TheGivenUsername()
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
    public void SetPassword_ShouldSet_TheGivenPassword()
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
    public void Credentials_ShouldSet_TheGivenUsernameAndPassword()
    {
        // Arrange
        var host     = "smtp.outlook.com";
        var port     = 2525;
        var mail     = new Mail( host , port );
        var username = "MBARK";
        var password = "123456";

        // Act
        mail.Credentials( username , password );
        var actualPrivateUsername = typeof(Mail).GetProperty("Username", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mail, null).ToString();
        var actualPrivatePassword = typeof(Mail).GetProperty("Password", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mail, null).ToString();
      
        // Assert
        Assert.Equal( actualPrivateUsername , username );
        Assert.Equal( actualPrivatePassword , password );
    }


    [ Fact ]
    public void From_ShouldSet_TheGivenEmail()
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
    
    [ Theory ]
    [InlineData( "mb ark@outlook.com")]
    [InlineData( "mb;ark@outlook.com")]
    [InlineData( "mbark$outlook.com")]
    [InlineData( "mbark outlook.com")]
    [InlineData( "mbark@outlookcom")]
    [InlineData( "mbark@outlook")]
    public void From_With_InvalidEmails_Should_ThrowException(string sender)
    {
        // Arrange
        var host        = "smtp.outlook.com";
        var port        = 2525;
        var mail        = new Mail(host, port);

        // Act

        // Assert
        Assert.Throws<EmailIsNotValidException>( () => mail.From( sender ) );
    }   
    
    [ Theory ]
    [InlineData( "mbark@outlook.com")]
    [InlineData( "MbarkT3sto@outlook.com")]
    [InlineData( "mbark$@outlook.com")]
    public void From_With_ValidEmails_Should_SetTheEmails(string sender)
    {
        // Arrange
        var host = "smtp.outlook.com";
        var port = 2525;
        var mail = new Mail( host , port );

        // Act
        mail.From(sender);
        var currentSenderEmail = mail.MailMessage.From.Address;

        // Assert
        currentSenderEmail.Should().Be(sender);
    }


    [ Fact ]
    public void To_ShouldSet_TheGivenEmail()
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
    public void To_ShouldSet_TheGivenEmails()
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


    [Fact]
    public void Subject_ShouldSet_TheGivenSubject()
    {
        // Arrange
        var host    = "smtp.outlook.com";
        var port    = 2525;
        var mail    = new Mail(host, port);
        var subject = "Test the Subject method";

        // Act
        mail.Subject( subject );
        var currentSubject = mail.MailMessage.Subject;

        // Assert
        currentSubject.Should().Be( subject );
    }


    [Fact]
    public void Body_ShouldSet_TheGivenBody()
    {
        // Arrange
        var host = "smtp.outlook.com";
        var port = 2525;
        var mail = new Mail(host, port);
        var body = "Test the Body method";

        // Act
        mail.Body( body );
        var actualBody = mail.MailMessage.Body;

        // Assert
        actualBody.Should().Be( body );
    }


    [Fact]
    public void IsBodyHtml_ShouldSet_TheGivenBooleanValue()
    {
        // Arrange
        var host = "smtp.outlook.com";
        var port = 2525;
        var mail = new Mail(host, port);
        var body = "<h1>Test the IsBodyHtml method</h1>";

        // Act
        mail.Body( body ).IsBodyHtml( true );
        var actualIsBodyHtml = mail.MailMessage.IsBodyHtml;

        // Assert
        actualIsBodyHtml.Should().Be( true );
    }


    [Fact]
    public void EnableSsl_Should_EnableSsl()
    {
        // Arrange
        var host = "smtp.outlook.com";
        var port = 2525;
        var mail = new Mail(host, port);

        // Act
        mail.EnableSsl();
        var actualStatOfSsl = mail.Client.EnableSsl;

        // Assert
        actualStatOfSsl.Should().Be( true );
    }  
    
    [Fact]
    public void DisableSsl_Should_DisableSsl()
    {
        // Arrange
        var host = "smtp.outlook.com";
        var port = 2525;
        var mail = new Mail(host, port);

        // Act
        mail.DisableSsl();
        var actualStatOfSsl = mail.Client.EnableSsl;

        // Assert
        actualStatOfSsl.Should().Be( false );
    }


    [Fact]
    public void Attachments_With_StreamParameter_Should_SetTheAttachment()
    {
        // Arrange
        var host = "smtp.outlook.com";
        var port = 2525;
        var mail = new Mail(host, port);

        var filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+"\\VLOOKUP.xlsx";
        var fileAsStream = File.Open( filePath, FileMode.Open);

        // Act
        mail.Attachments( fileAsStream , MediaTypeNames.Application.Octet );
        var actualAttachmentsCount = mail.MailMessage.Attachments.Count;

        // Assert
        actualAttachmentsCount.Should().Be( 1 );
    }

}