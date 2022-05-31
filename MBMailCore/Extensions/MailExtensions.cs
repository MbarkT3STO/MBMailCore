using System.Net.Mail;
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
    /// Determines the email sender
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="sender">Email of the sender</param>
    public static Mail From(this Mail mail, string sender)
    {
        mail.MailMessage.From = new MailAddress( sender );
        return mail;
    }
}