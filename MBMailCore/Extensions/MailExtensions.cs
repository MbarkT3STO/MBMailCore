using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Text.RegularExpressions;
using MBMailCore.Core;
using MBMailCore.Enums;
using MBMailCore.Exceptions;

namespace MBMailCore.Extensions;

public static class MailExtensions
{
    #region Private methods

    /// <summary>
    /// Returns the MIME Type based on file extension
    /// </summary>
    /// <param name="extension">File extension</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private static string GetMimeType(FileExtension extension)
    {
        return extension switch
               {
                   FileExtension.aac            => "audio/aac" ,
                   FileExtension.abw            => "application/x-abiword" ,
                   FileExtension.arc            => "application/x-freearc" ,
                   FileExtension.avif           => "image/avif" ,
                   FileExtension.avi            => "video/x-msvideo" ,
                   FileExtension.azw            => "application/vnd.amazon.ebook" ,
                   FileExtension.bin            => "application/octet-stream" ,
                   FileExtension.bmp            => "image/bmp" ,
                   FileExtension.bz             => "application/x-bzip" ,
                   FileExtension.bz2            => "application/x-bzip2" ,
                   FileExtension.cda            => "application/x-cdf" ,
                   FileExtension.csh            => "application/x-csh" ,
                   FileExtension.css            => "text/css" ,
                   FileExtension.csv            => "text/csv" ,
                   FileExtension.doc            => "application/msword" ,
                   FileExtension.docx           => "application/vnd.openxmlformats-officedocument.wordprocessingml.document" ,
                   FileExtension.eot            => "application/vnd.ms-fontobject" ,
                   FileExtension.epub           => "application/epub+zip" ,
                   FileExtension.gz             => "application/gzip" ,
                   FileExtension.gif            => "image/gif" ,
                   FileExtension.htm            => "text/html" ,
                   FileExtension.html           => "text/html" ,
                   FileExtension.ico            => "image/vnd.microsoft.icon" ,
                   FileExtension.ics            => "text/calendar" ,
                   FileExtension.jar            => "application/java-archive" ,
                   FileExtension.jpeg           => "image/jpeg" ,
                   FileExtension.jpg            => "image/jpeg" ,
                   FileExtension.js             => "text/javascript" ,
                   FileExtension.json           => "application/json" ,
                   FileExtension.jsonld         => "application/ld+json" ,
                   FileExtension.mid            => "audio/midi" ,
                   FileExtension.midi           => "audio/x-midi" ,
                   FileExtension.mjs            => "text/javascript" ,
                   FileExtension.mp3            => "audio/mpeg" ,
                   FileExtension.mp4            => "video/mp4" ,
                   FileExtension.mpeg           => "video/mpeg" ,
                   FileExtension.mpkg           => "application/vnd.apple.installer+xml" ,
                   FileExtension.odp            => "application/vnd.oasis.opendocument.presentation" ,
                   FileExtension.ods            => "application/vnd.oasis.opendocument.spreadsheet" ,
                   FileExtension.odt            => "application/vnd.oasis.opendocument.text" ,
                   FileExtension.oga            => "audio/ogg" ,
                   FileExtension.ogv            => "video/ogg" ,
                   FileExtension.ogx            => "application/ogg" ,
                   FileExtension.opus           => "audio/opus" ,
                   FileExtension.otf            => "font/otf" ,
                   FileExtension.png            => "image/png" ,
                   FileExtension.pdf            => "application/pdf" ,
                   FileExtension.php            => "application/x-httpd-php" ,
                   FileExtension.ppt            => "application/vnd.ms-powerpoint" ,
                   FileExtension.pptx           => "application/vnd.openxmlformats-officedocument.presentationml.presentation" ,
                   FileExtension.rar            => "application/vnd.rar" ,
                   FileExtension.rtf            => "application/rtf" ,
                   FileExtension.sh             => "application/x-sh" ,
                   FileExtension.svg            => "image/svg+xml" ,
                   FileExtension.swf            => "application/x-shockwave-flash" ,
                   FileExtension.tar            => "application/x-tar" ,
                   FileExtension.tif            => "image/tiff" ,
                   FileExtension.tiff           => "image/tiff" ,
                   FileExtension.ts             => "video/mp2t" ,
                   FileExtension.ttf            => "font/ttf" ,
                   FileExtension.txt            => "text/plain" ,
                   FileExtension.vsd            => "application/vnd.visio" ,
                   FileExtension.wav            => "audio/wav" ,
                   FileExtension.weba           => "audio/webm" ,
                   FileExtension.webm           => "video/webm" ,
                   FileExtension.webp           => "image/webp" ,
                   FileExtension.woff           => "font/woff" ,
                   FileExtension.woff2          => "font/woff2" ,
                   FileExtension.xhtml          => "application/xhtml+xml" ,
                   FileExtension.xls            => "application/vnd.ms-excel" ,
                   FileExtension.xlsx           => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" ,
                   FileExtension.xml            => "application/xml" ,
                   FileExtension.xul            => "application/vnd.mozilla.xul+xml" ,
                   FileExtension.zip            => "application/zip" ,
                   FileExtension._3gp           => "video/3gpp" ,
                   FileExtension._3gp_OnlyAudio => "audio/3gpp" ,
                   FileExtension._3g2           => "video/3gpp2" ,
                   FileExtension._3g2_OnlyAudio => "audio/3gpp2" ,
                   FileExtension._7z            => "application/x-7z-compressed" ,
                   _                            => throw new ArgumentOutOfRangeException( nameof( extension ) , extension , null )
               };
    }

    /// <summary>
    /// Checks if a text is a valid email
    /// </summary>
    /// <param name="value">Text to be checked</param>
    private static bool IsValidEmail( string value )
    {
        var trimmedValue = value.Trim();
        const string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                               + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                               + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        var regex = new Regex( pattern , RegexOptions.IgnoreCase );

        return regex.IsMatch( trimmedValue );
    }

    #endregion

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
    /// Set the <b>credentials</b> of the mail sender
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="username">Username</param>
    /// <param name="password">Password</param>
    public static Mail Credentials(this Mail mail, string username, string password)
    {
        SetUsername(mail, username );
        SetPassword(mail, password );

        mail.Client.Credentials = new NetworkCredential( username , password );

        return mail;
    }

    /// <summary>
    /// Set the <b>credentials</b> of the mail sender
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="credential">Sender credentials</param>
    public static Mail Credentials(this Mail mail, NetworkCredential credential)
    {
        SetUsername( mail , credential.UserName );
        SetPassword( mail , credential.Password );

        mail.Client.Credentials = credential;

        return mail;
    }

    /// <summary>
    /// Enables the SSL
    /// </summary>
    /// <param name="mail"></param>
    public static Mail EnableSsl(this Mail mail)
    {
        mail.Client.EnableSsl = true;

        return mail;
    }
      
    /// <summary>
    /// Disables the SSL
    /// </summary>
    /// <param name="mail"></param>
    public static Mail DisableSsl(this Mail mail)
    {
        mail.Client.EnableSsl = false;

        return mail;
    }


    /// <summary>
    /// Determines the email sender
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="sender">Email of the sender</param>
    /// <exception cref="EmailIsNotValidException"></exception>
    public static Mail From(this Mail mail, string sender)
    {
        // Throw exception if the email(sender) is not valid
        if ( !IsValidEmail( sender ) ) throw new EmailIsNotValidException( sender );

        mail.MailMessage.From = new MailAddress( sender );
        return mail;
    }


    /// <summary>
    /// Determines the email receiver
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="receiver">Email of the receiver</param>
    public static Mail To(this Mail mail, string receiver)
    {
        // Throw exception if the email(receiver) is not valid
        if (!IsValidEmail(receiver)) throw new EmailIsNotValidException(receiver);

        mail.MailMessage.To.Add( receiver );

        return mail;
    }
    
    /// <summary>
    /// Determines the email receivers
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="receivers">Emails of the receivers</param>
    public static Mail To(this Mail mail, List<string> receivers)
    {
        receivers.ForEach( receiver =>
                           {
                               // Throw exception if the email(receiver) is not valid
                               if (!IsValidEmail(receiver)) throw new EmailIsNotValidException(receiver);

                               mail.MailMessage.To.Add( receiver );
                           } );

        return mail;
    }


    /// <summary>
    /// Determines the email's subject
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="subject">Email's subject</param>
    public static Mail Subject(this Mail mail, string subject)
    {
        mail.MailMessage.Subject = subject;

        return mail;
    }


    /// <summary>
    /// Determines the email's body
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="body">Email's body</param>
    public static Mail Body(this Mail mail, string body)
    {
        mail.MailMessage.Body = body;

        return mail;
    }


    /// <summary>
    /// Determines the email's body
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="body">Email's body</param>
    /// <param name="isBodyHtml"><b>True</b> if the body is HTML</param>
    public static Mail Body(this Mail mail, string body, bool isBodyHtml)
    {
        mail.MailMessage.Body = body;

        IsBodyHtml( mail , isBodyHtml );

        return mail;
    }

    /// <summary>
    /// Determines if the email's body is HTML
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="isBodyHtml"><b>True</b> if the body is HTML</param>
    public static Mail IsBodyHtml(this Mail mail, bool isBodyHtml)
    {
        mail.MailMessage.IsBodyHtml = isBodyHtml;

        return mail;
    }


    /// <summary>
    /// Determines the email's attachment
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="attachment">Email's attachment</param>
    public static Mail Attachments(this Mail mail, Attachment attachment)
    {
        mail.MailMessage.Attachments.Add( attachment );

        return mail;
    }


    /// <summary>
    /// Determines the email's attachment
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="attachmentStream">Email's attachment as <see cref="Stream"/></param>
    /// <param name="mediaType">The attachment media type</param>
    public static Mail Attachments(this Mail mail, Stream attachmentStream, string mediaType)
    {
        var attachment = new Attachment(attachmentStream, new ContentType(mediaType));
        mail.MailMessage.Attachments.Add(attachment);

        return mail;
    }


    /// <summary>
    /// Determines the email's attachment
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="attachmentStream">Email's attachment as <see cref="Stream"/></param>
    /// <param name="fileExtension">The attachment media type / File extension</param>
    public static Mail Attachments(this Mail mail, Stream attachmentStream, FileExtension fileExtension)
    {
        var mediaType  = GetMimeType( fileExtension );
        var attachment = new Attachment( attachmentStream , new ContentType( mediaType ) );
        mail.MailMessage.Attachments.Add( attachment );

        return mail;
    }

    /// <summary>
    /// Determines the email attachments
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="attachments">Email attachments</param>
    public static Mail Attachments(this Mail mail, ICollection<Attachment> attachments)
    {
        foreach ( Attachment attachment in attachments )
        {
            mail.MailMessage.Attachments.Add( attachment );
        }

        return mail;
    }


    /// <summary>
    /// Sends the email
    /// </summary>
    /// <param name="mail"></param>
    public static void Send(this Mail mail)
    {
        mail.Client.Send( mail.MailMessage );
    }


    /// <summary>
    /// Sends the email asynchronously
    /// </summary>
    /// <param name="mail"></param>
    public static Task SendAsync(this Mail mail)
    {
        return mail.Client.SendMailAsync( mail.MailMessage );
    }
}