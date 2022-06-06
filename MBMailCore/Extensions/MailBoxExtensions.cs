using GemBox.Email.Pop;
using MBMailCore.Core;

namespace MBMailCore.Extensions;

public static class MailBoxExtensions
{
    /// <summary>
    /// Indicates the email provider's host
    /// </summary>
    /// <returns></returns>
    public static MailBox Host(this MailBox mailBox, string host)
    {
        mailBox.PopClient = new PopClient( host );

        return mailBox;
    }
}