using System.Net.Mail;

namespace MBMailCore.Core;

public class Mail : SmtpClient
{
    private string      Username    { get; set; }
    private string      Password    { get; set; }
    public  MailMessage MailMessage { get; set; } = new();

    public Mail(string host, int port)
    {
        this.Host = host;
        this.Port = port;
    }
}