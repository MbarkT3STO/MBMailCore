
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using MBMailCore.Core;
using MBMailCore.Enums;
using MBMailCore.Extensions;
using GemBox.Email.Pop;

var host = "smtp-mail.outlook.com";
var port = 587;

// Create mail object
var mail = new Mail( host , port );

// Credentials
var credentials = new NetworkCredential( "mbarkdev@outlook.com" , "X@123456@X" );

#region Global test N1 [ Passed ]

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

#region Global test N2 [ Passed ]

//Console.WriteLine("Sending...");

//var attachment = new Attachment(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\TP.pdf");

//// Mail
//mail.Credentials( credentials )
//    .EnableSsl()
//    .From( "mbarkdev@outlook.com" )
//    .To( "mbarktiesto@outlook.com" )
//    .Subject( "Global test N2" )
//    .Body( "Hello dear from the global test N2" )
//    .Attachments( attachment )
//    .Send();

//Console.WriteLine("Sent");

#endregion 

#region Global test N3 [ Failed ]

//Console.WriteLine("Sending...");

//var attachment = new Attachment(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\TP.pdf");

//var filePath     = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\VLOOKUP.xlsx";
//var fileAsStream = File.Open( filePath , FileMode.Open );


//// Mail
//mail.Credentials( credentials )
//    .EnableSsl()
//    .From( "mbarkdev@outlook.com" )
//    .To( "mbarktiesto@outlook.com" )
//    .Subject( "Global test N3" )
//    .Body( "Hello dear from the global test N3" )
//    .Attachments( attachment )
//    .Attachments( fileAsStream , MediaTypeNames.Application.Octet)
//    .Send();

//Console.WriteLine("Sent");

#endregion

#region Global test N4 [ Passed ]

//Console.WriteLine("Sending...");

//var attachment = new Attachment(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\TP.pdf");

//var filePath     = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Practice data.xlsx";
//var fileAsStream = File.Open(filePath, FileMode.Open);


//// Mail
//mail.Credentials(credentials)
//    .EnableSsl()
//    .From("mbarkdev@outlook.com")
//    .To("mbarktiesto@outlook.com")
//    .Subject("Global test N4")
//    .Body("Hello dear from the global test N4")
//    .Attachments(attachment)
//    .Attachments(fileAsStream, "application/vnd.ms-excel")
//    .Send();

//Console.WriteLine("Sent");

#endregion

#region Global test N5 [ Passed ]

//Console.WriteLine("Sending...");

//var attachment = new Attachment(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\TP.pdf");

//var filePath     = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Practice data.xlsx";
//var fileAsStream = File.Open(filePath, FileMode.Open);


//// Mail
//mail.Credentials( credentials )
//    .EnableSsl()
//    .From( "mbarkdev@outlook.com" )
//    .To( "mbarktiesto@outlook.com" )
//    .Subject( "Global test N5" )
//    .Body( "Hello dear from the global test N5" )
//    .Attachments( attachment )
//    .Attachments( fileAsStream , FileExtension.xlsx )
//    .Send();

//Console.WriteLine("Sent");

#endregion

#region Global test N6 [ Passed ]

//Console.WriteLine("Connecting...");

//// Mail Box
//var mailBox = new MailBox();
//mailBox.Host( "outlook.office365.com", 995 ).Authenticate( "mbarkdev@outlook.com" , "X@123456@X" );

//Console.WriteLine("Connected...");

//var lastReceivedEmail = mailBox.GetLastReceivedMail();

//Console.WriteLine( "Last received mail :" );
//Console.WriteLine($"Sender : {lastReceivedEmail.From[0].Address}");
//Console.WriteLine($"Subject : {lastReceivedEmail.Subject}");
//Console.WriteLine( $"Body : {lastReceivedEmail.BodyText}" );

#endregion

#region Global test N7 [ Failed ]

Console.WriteLine("Connecting...");

// Mail Box
var mailBox = new MailBox();
mailBox.Host("outlook.office365.com", 995).Authenticate("mbarkdev@outlook.com", "X@123456@X");

Console.WriteLine("Connected...");

var numberofLastReceivedEmailFromMbark = mailBox.SearchMessageNumbers( "FROM mbarktiesto@outlook.com" ).FirstOrDefault();
var LastReceivedEmailFromMbark = mailBox.GetMail( numberofLastReceivedEmailFromMbark );

Console.WriteLine("Last received mail from MBARK TIESTO :");
Console.WriteLine($"Sender : {LastReceivedEmailFromMbark.From[0].Address}");
Console.WriteLine($"Subject : {LastReceivedEmailFromMbark.Subject}");
Console.WriteLine($"Body : {LastReceivedEmailFromMbark.BodyText}");

#endregion

Console.ReadKey();