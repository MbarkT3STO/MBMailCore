using System.Net.Mail;
using GemBox.Email.Pop;

namespace MBMailCore.Core;

/// <summary>
/// Represents an Email Box
/// </summary>
public class MailBox
{
    #region Properties

    public PopClient PopClient { get; set; }

    #endregion

    #region Constructors

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