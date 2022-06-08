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

    //private static void AuthenticateImapClient(MailBox mailBox)
    //{
    //    if ( ! mailBox.ImapClient.IsConnected && ! mailBox.ImapClient.IsAuthenticated )
    //    {
    //        mailBox.ImapClient = new ImapClient(mailBox.Host().Host, mailBox.ImapClient.Port);

    //        mailBox.ImapClient.Connect();
    //        mailBox.ImapClient.Authenticate(mailBox.Username, mailBox.Password);
    //    }
    //}

    #endregion

    /// <summary>
    /// Indicates the email provider's host
    /// </summary>
    /// <param name="mailBox"></param>
    /// <param name="host">Host</param>
    public static MailBox Host(this MailBox mailBox, string host)
    {
        mailBox.ImapClient = new ImapClient( host );

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
        mailBox.ImapClient = new ImapClient( host , port );

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

        mailBox.ImapClient.Connect();
        mailBox.ImapClient.Authenticate( username , password);

        return mailBox;
    }


    /// <summary>
    /// Selects the inbox
    /// </summary>
    /// <param name="mailBox"></param>
    public static MailBox SelectInbox(this MailBox mailBox)
    {
        mailBox.ImapClient.SelectInbox();

        return mailBox;
    }   
    
    /// <summary>
    /// Selects a specific folder
    /// </summary>
    /// <param name="mailBox"></param>
    /// <param name="folderName">Folder name</param>
    /// <param name="readOnly">Indicates the manipulation mode with the folder</param>
    public static MailBox SelectFolder(this MailBox mailBox, string folderName, bool readOnly)
    {
        mailBox.ImapClient.SelectFolder( folderName , readOnly );

        return mailBox;
    }


    /// <summary>
    /// Get the last received email message
    /// </summary>
    /// <param name="mailBox"></param>
    public static MailMessage GetLastReceivedMail(this MailBox mailBox)
    {
        var messagesCount = mailBox.ImapClient.ListMessages().Count;
        var lastReceivedMail = mailBox.ImapClient.GetMessage( messagesCount - 1 );

        return lastReceivedMail;
    }


    /// <summary>
    /// Get a specific mail message
    /// </summary>
    /// <param name="mailBox"></param>
    /// <param name="messageNumber">The Number/Index of the message/mail</param>
    public static MailMessage GetMail(this MailBox mailBox, int messageNumber)
    {
        var mailMessage = mailBox.ImapClient.GetMessage( messageNumber );

        return mailMessage;
    }

    /// <summary>
    /// Get the received Emails count
    /// </summary>
    /// <param name="mailBox"></param>
    public static int Count(this MailBox mailBox )
    {
        var emailsCount = mailBox.ImapClient.ListMessages().Count;

        return emailsCount;
    }


    /// <summary>
    /// Search for mail message numbers
    /// </summary>
    /// <param name="mailBox"></param>
    /// <param name="query">Search pattern/query</param>
    public static ReadOnlyCollection<int> SearchMailNumbers(this MailBox mailBox, string query)
    {
        var result = mailBox.ImapClient.SearchMessageNumbers( query );

        return result;
    }

    /// <summary>
    /// Search for mail  messages
    /// </summary>
    /// <param name="mailBox"></param>
    /// <param name="query">Search pattern/query</param>
    public static Collection<MailMessage> SearchMails(this MailBox mailBox, string query)
    {
        var messageNumbers = mailBox.ImapClient.SearchMessageNumbers( query );
        var result         = new Collection<MailMessage>();

        foreach ( var number in messageNumbers )
        {
            var message = mailBox.ImapClient.GetMessage( number );
            result.Add(message);
        }

        return result;
    }

    /// <summary>
    /// <inheritdoc cref="SearchMails"/>
    /// </summary>
    /// <inheritdoc cref="SearchMails"/>
    public static Task<Collection<MailMessage>> SearchMailsAsync( this MailBox mailBox , string query )
    {
        return Task.Run( () => SearchMails( mailBox , query ) );
    }


    /// <summary>
    /// Returns the last received mail from a specific sender
    /// </summary>
    /// <param name="mailBox"></param>
    /// <param name="sender">The sender's email address</param>
    public static MailMessage ? GetLastReceivedMailFrom(this MailBox mailBox, string sender)
    {
        var messageNumber = mailBox.ImapClient.SearchMessageNumbers( $"FROM {sender}" ).LastOrDefault( -1 );

        MailMessage? message = null;

        if ( messageNumber != -1 )
        { 
            message = mailBox.ImapClient.GetMessage( messageNumber );
        }

        return message;
    } 
    
    /// <summary>
    /// Returns all received mails from a specific sender
    /// </summary>
    /// <param name="mailBox"></param>
    /// <param name="sender">The sender's email address</param>
    public static ICollection<MailMessage> GetAllReceivedMailsFrom(this MailBox mailBox, string sender)
    {
        var searchMessageNumbers = mailBox.ImapClient.SearchMessageNumbers( $"FROM {sender}" );

        ICollection<MailMessage> messages = new Collection<MailMessage>();

        if ( searchMessageNumbers.Count > 0 )
        {
            foreach ( var messageNumber in searchMessageNumbers )
            {
                var message = mailBox.ImapClient.GetMessage( messageNumber );

                messages.Add( message );
            }
        }

        return messages;
    }

    /// <summary>
    /// <inheritdoc cref="GetAllReceivedMailsFrom(MailBox,string)"/>
    /// </summary>
    /// <inheritdoc cref="GetAllReceivedMailsFrom(MailBox,string)"/>
    public static Task<ICollection<MailMessage>> GetAllReceivedMailsFromAsync(this MailBox mailBox, string sender)
    {
        return Task.Run( () => mailBox.GetAllReceivedMailsFrom( sender ) );
    }
}