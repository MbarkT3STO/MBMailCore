using GemBox.Email.Pop;
using MBMailCore.Core;

namespace MBMailCore.Extensions;

public static class MailBoxExtensions
{
    /// <summary>
    /// Indicates the email provider's host
    /// </summary>
    /// <param name="mailBox"></param>
    /// <param name="host">Host</param>
    public static MailBox Host(this MailBox mailBox, string host)
    {
        mailBox.PopClient = new PopClient( host );

        return mailBox;
    }

    /// <summary>
    /// Indicates the email provider's host and port
    /// </summary>
    /// <param name="mailBox"></param>
    /// <param name="host">Host</param>
    /// <param name="port">Port</param>
    public static MailBox Host(this MailBox mailBox, string host, int port)
    {
        mailBox.PopClient = new PopClient( host , port );

        return mailBox;
    }
}