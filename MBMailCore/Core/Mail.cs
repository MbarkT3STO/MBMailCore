using System.Net;
using System.Net.Mail;
using MBMailCore.Extensions;

namespace MBMailCore.Core;

public class Mail
{
    #region Properties

    private string Username { get; set; }
    private string Password { get; set; }

    public SmtpClient  Client      { get; set; }
    public MailMessage MailMessage { get; set; } = new();

    #endregion

    #region Constructors

    public Mail(string host, int port)
    {
        Client = new SmtpClient( host , port );
    }  
    public Mail(string host, int port, string username, string password)
    {
        Client = new SmtpClient( host , port );

        this.Credentials( username , password );
    } 
    public Mail(string host, int port, NetworkCredential credential)
    {
        Client = new SmtpClient( host , port );

        this.Credentials( credential );
    }

    #endregion
}