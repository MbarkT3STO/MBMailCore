using System.Net.Mail;

namespace MBMailCore.Core;

public class Mail
{
    private string      Username    { get; set; }
    private string      Password    { get; set; }

    public SmtpClient  Client      { get; set; }
    public MailMessage MailMessage { get; set; } = new();

    public Mail(string host, int port)
    {
        Client = new SmtpClient( host , port );
    }
}