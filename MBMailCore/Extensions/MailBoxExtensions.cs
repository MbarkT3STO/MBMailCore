using GemBox.Email.Pop;
using MBMailCore.Core;
using MailMessage = GemBox.Email.MailMessage;

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


    /// <summary>
    /// Authenticates the user to his email box
    /// </summary>
    /// <param name="mailBox"></param>
    /// <param name="username">Username</param>
    /// <param name="password">Password</param>
    public static MailBox Authenticate(this MailBox mailBox, string username, string password)
    {
        mailBox.PopClient.Authenticate( username , password );

        return mailBox;
    } 
    
    /// <summary>
    /// Get the last received email message
    /// </summary>
    /// <param name="mailBox"></param>
    public static MailMessage GetLastReceivedMail(this MailBox mailBox)
    {
        var messagesCount    = mailBox.PopClient.GetCount();
        var lastReceivedMail = mailBox.PopClient.GetMessage( messagesCount -1 );

        return lastReceivedMail;
    }
}