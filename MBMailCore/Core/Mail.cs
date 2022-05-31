using System.Net.Mail;

namespace MBMailCore.Core;

public class Mail : SmtpClient
{
    private string Username { get; set; }
    private string Password { get; set; }
}