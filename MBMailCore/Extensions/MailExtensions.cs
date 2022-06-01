﻿using System.Net;
using System.Net.Mail;
using System.Reflection;
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
    /// Determines the email sender
    /// </summary>
    /// <param name="mail"></param>
    /// <param name="sender">Email of the sender</param>
    public static Mail From(this Mail mail, string sender)
    {
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
        receivers.ForEach( receiver => mail.MailMessage.To.Add( receiver ) );

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
    /// <param name="isBodyHtml">Determines if the email's body is HTML</param>
    public static Mail Body(this Mail mail, bool isBodyHtml)
    {
        mail.MailMessage.IsBodyHtml = isBodyHtml;

        return mail;
    }
}