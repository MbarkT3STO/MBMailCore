using System.Net.Mail;
using GemBox.Email.Imap;
using GemBox.Email.Pop;

namespace MBMailCore.Core;

/// <summary>
/// Represents an Email Box
/// </summary>
public class MailBox
{
    #region Properties

    public string Username { get; set; }
    public string Password { get; set; }

    public PopClient  PopClient  { get; set; }
    public ImapClient ImapClient { get; set; }

    #endregion

    #region Constructors

    public MailBox()
    {
        
    }
    public MailBox(string host)
    {
        PopClient = new PopClient( host );
    } 
    
    public MailBox(string host, int port)
    {
        PopClient = new PopClient( host , port );
    }

    #endregion
}