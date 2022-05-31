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
        mail.GetType().GetProperty( "Username" )?.SetValue( mail , username );

        return mail;
    }

    /// <summary>
    /// Set the <b>Password</b> of the mail sender
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="password">Password</param>
    public static Mail SetPassword(this Mail mail, string password)
    {
        mail.GetType().GetProperty("Username")?.SetValue(mail, password);

        return mail;
    }
}