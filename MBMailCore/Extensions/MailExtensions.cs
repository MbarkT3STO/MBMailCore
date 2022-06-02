using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using MBMailCore.Core;

namespace MBMailCore.Extensions;

public static class MailExtensions
{
    /// <summary>
    /// Set the <b>username</b> of the mail sender
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="username">Username</param>
    public static Mail SetUsername(this Mail mail, string username)
    {
        typeof(Mail).GetProperty("Username", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue( mail , username );

        return mail;
    }

    /// <summary>
    /// Set the <b>Password</b> of the mail sender
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="password">Password</param>
    public static Mail SetPassword(this Mail mail, string password)
    {
        typeof(Mail).GetProperty("Password", BindingFlags.NonPublic | BindingFlags.Instance)?.SetValue(mail, password);

        return mail;
    }

    /// <summary>
    /// Set the <b>credentials</b> of the mail sender
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="username">Username</param>
    /// <param name="password">Password</param>
    public static Mail Credentials(this Mail mail, string username, string password)
    {
        SetUsername(mail, username );
        SetPassword(mail, password );

        mail.Client.Credentials = new NetworkCredential( username , password );

        return mail;
    }

    /// <summary>
    /// Set the <b>credentials</b> of the mail sender
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="credential">Sender credentials</param>
    public static Mail Credentials(this Mail mail, NetworkCredential credential)
    {
        SetUsername( mail , credential.UserName );
        SetPassword( mail , credential.Password );

        mail.Client.Credentials = credential;

        return mail;
    }

    /// <summary>
    /// Enables the SSL
    /// </summary>
    /// <param name="mail"></param>
    public static Mail EnableSsl(this Mail mail)
    {
        mail.Client.EnableSsl = true;

        return mail;
    }
      
    /// <summary>
    /// Disables the SSL
    /// </summary>
    /// <param name="mail"></param>
    public static Mail DisableSsl(this Mail mail)
    {
        mail.Client.EnableSsl = false;

        return mail;
    }


    /// <summary>
    /// Determines the email sender
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="sender">Email of the sender</param>
    public static Mail From(this Mail mail, string sender)
    {
        mail.MailMessage.From = new MailAddress( sender );
        return mail;
    }


    /// <summary>
    /// Determines the email receiver
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="receiver">Email of the receiver</param>
    public static Mail To(this Mail mail, string receiver)
    {
        mail.MailMessage.To.Add( receiver );

        return mail;
    }
    
    /// <summary>
    /// Determines the email receivers
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="receivers">Emails of the receivers</param>
    public static Mail To(this Mail mail, List<string> receivers)
    {
        receivers.ForEach( receiver => mail.MailMessage.To.Add( receiver ) );

        return mail;
    }


    /// <summary>
    /// Determines the email's subject
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="subject">Email's subject</param>
    public static Mail Subject(this Mail mail, string subject)
    {
        mail.MailMessage.Subject = subject;

        return mail;
    }


    /// <summary>
    /// Determines the email's body
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="body">Email's body</param>
    public static Mail Body(this Mail mail, string body)
    {
        mail.MailMessage.Body = body;

        return mail;
    }


    /// <summary>
    /// Determines the email's body
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="body">Email's body</param>
    /// <param name="isBodyHtml"><b>True</b> if the body is HTML</param>
    public static Mail Body(this Mail mail, string body, bool isBodyHtml)
    {
        mail.MailMessage.Body = body;

        IsBodyHtml( mail , isBodyHtml );

        return mail;
    }

    /// <summary>
    /// Determines if the email's body is HTML
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="isBodyHtml"><b>True</b> if the body is HTML</param>
    public static Mail IsBodyHtml(this Mail mail, bool isBodyHtml)
    {
        mail.MailMessage.IsBodyHtml = isBodyHtml;

        return mail;
    }


    /// <summary>
    /// Determines the email's attachment
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="attachment">Email's attachment</param>
    public static Mail Attachments(this Mail mail, Attachment attachment)
    {
        mail.MailMessage.Attachments.Add( attachment );

        return mail;
    }


    /// <summary>
    /// Determines the email's attachment
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="attachmentStream">Email's attachment as <see cref="Stream"/></param>
    /// <param name="mediaType">The attachment media type</param>
    public static Mail Attachments(this Mail mail, Stream attachmentStream, string mediaType)
    {
        var attachment = new Attachment( attachmentStream , new ContentType( mediaType ) );
        mail.MailMessage.Attachments.Add( attachment );

        return mail;
    }

    /// <summary>
    /// Determines the email attachments
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="attachments">Email attachments</param>
    public static Mail Attachments(this Mail mail, ICollection<Attachment> attachments)
    {
        foreach ( Attachment attachment in attachments )
        {
            mail.MailMessage.Attachments.Add( attachment );
        }

        return mail;
    }


    /// <summary>
    /// Sends the email
    /// </summary>
    /// <param name="mail"></param>
    public static void Send(this Mail mail)
    {
        mail.Client.Send( mail.MailMessage );
    }


    /// <summary>
    /// Sends the email asynchronously
    /// </summary>
    /// <param name="mail"></param>
    public static Task SendAsync(this Mail mail)
    {
        return mail.Client.SendMailAsync( mail.MailMessage );
    }
}