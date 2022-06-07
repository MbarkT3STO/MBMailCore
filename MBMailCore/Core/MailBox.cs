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

    /// <summary>
    /// Until this moment this represents an (GemBox) IMAP client
    /// </summary>
    public ImapClient ImapClient { get; set; }

    #endregion

    #region Constructors

    public MailBox()
    {
        
    }
    public MailBox(string host)
    {
        ImapClient = new ImapClient( host );
    } 
    
    public MailBox(string host, int port)
    {
        ImapClient = new ImapClient( host , port );
    }

    #endregion
}