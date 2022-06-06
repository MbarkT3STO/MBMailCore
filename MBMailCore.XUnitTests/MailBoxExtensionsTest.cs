using FluentAssertions;
using MBMailCore.Core;
using MBMailCore.Extensions;

namespace MBMailCore.XUnitTests;

public class MailBoxExtensionsTest
{
    [Fact]
    public void Host_With_Host_Should_Set_TheHostValue()
    {
        // Arrange
        var host    = "outlook.office365.com";
        var mailBox = new MailBox();

        mailBox.Host( host );

        // Act
        var actualHost = mailBox.PopClient.Host;

        // Assert
        actualHost.Should().Be( host );
    }

    [Fact]
    public void Host_With_HostAndPort_Should_Set_TheHostAndPortValues()
    {
        // Arrange
        var host    = "outlook.office365.com";
        var port    = 995;
        var mailBox = new MailBox();

        mailBox.Host( host , port );

        // Act
        var actualHost = mailBox.PopClient.Host;

        // Assert
        actualHost.Should().Be(host);
    }
}