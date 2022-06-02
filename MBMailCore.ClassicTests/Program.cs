
using System.Net;
using System.Net.Mail;
using System.Reflection;
using MBMailCore.Core;
using MBMailCore.Extensions;

var host = "smtp-mail.outlook.com";
var port = 587;

// Create mail object
var mail = new Mail( host , port );

// Credentials
var credentials = new NetworkCredential( "mbarkdev@outlook.com" , "X@123456@X" );

#region Global test N1

//Console.WriteLine( "Sending..." );

//// Mail
//mail.Credentials( credentials )
//    .From( "mbarkdev@outlook.com" )
//    .To( "mbarktiesto@outlook.com" )
//    .Subject( "Global test N1" )
//    .Body( "Hello dear from the global test N1" )
//    .Send();

//Console.WriteLine("Sent");

#endregion

#region Global test N2

Console.WriteLine("Sending...");

var attachment = new Attachment(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\TP.pdf");

// Mail
mail.Credentials( credentials )
    .EnableSsl()
    .From( "mbarkdev@outlook.com" )
    .To( "mbarktiesto@outlook.com" )
    .Subject( "Global test N2" )
    .Body( "Hello dear from the global test N2" )
    .Attachments( attachment )
    .Send();

Console.WriteLine("Sent");

#endregion

Console.ReadKey();