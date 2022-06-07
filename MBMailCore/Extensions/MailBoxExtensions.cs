using System.Collections.ObjectModel;
using GemBox.Email.Imap;
using GemBox.Email.Pop;
using GemBox.Email.Security;
using MBMailCore.Core;
using MailMessage = GemBox.Email.MailMessage;

namespace MBMailCore.Extensions;

public static class MailBoxExtensions
{
    #region Private methods

    private static void AuthenticateImapClient(MailBox mailBox)
    {
        if ( ! mailBox.ImapClient.IsConnected && ! mailBox.ImapClient.IsAuthenticated )
        {
            mailBox.ImapClient = new ImapClient(mailBox.PopClient.Host, mailBox.PopClient.Port);

            mailBox.ImapClient.Connect();
            mailBox.ImapClient.Authenticate(mailBox.Username, mailBox.Password);
        }
    }

    #endregion

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
        mailBox.Username = username;
        mailBox.Password = password;

        mailBox.PopClient.Connect();
        mailBox.PopClient.Authenticate( username , password);

        return mailBox;
    } 
    
    /// <summary>
    /// Get the last received email message
    /// </summary>
    /// <param name="mailBox"></param>
    public static MailMessage GetLastReceivedMail(this MailBox mailBox)
    {
        var messagesCount    = mailBox.PopClient.GetCount();
        var lastReceivedMail = mailBox.PopClient.GetMessage( messagesCount - 1 );

        return lastReceivedMail;
    }


    /// <summary>
    /// Get a specific mail message
    /// </summary>
    /// <param name="mailBox"></param>
    /// <param name="messageNumber">The Number/Index of the message/mail</param>
    public static MailMessage GetMail(this MailBox mailBox, int messageNumber)
    {
        var mailMessage = mailBox.PopClient.GetMessage( messageNumber );

        return mailMessage;
    }

    /// <summary>
    /// Get the received Emails count
    /// </summary>
    /// <param name="mailBox"></param>
    public static int Count(this MailBox mailBox )
    {
        var emailsCount = mailBox.PopClient.GetCount();

        return emailsCount;
    }


    /// <summary>
    /// Search for message numbers
    /// </summary>
    /// <param name="mailBox"></param>
    /// <param name="query">Search pattern/query</param>
    public static ReadOnlyCollection<int> SearchMessageNumbers(this MailBox mailBox, string query)
    {
        mailBox.ImapClient = new ImapClient( mailBox.PopClient.Host , mailBox.PopClient.Port );

        AuthenticateImapClient( mailBox );

        var result = mailBox.ImapClient.SearchMessageNumbers( query );

        return result;
    }
}