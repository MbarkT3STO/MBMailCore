
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using MBMailCore.Core;
using MBMailCore.Extensions;

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

Console.WriteLine("Sending...");

var attachment = new Attachment(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\TP.pdf");

var filePath     = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Practice data.xlsx";
var fileAsStream = File.Open(filePath, FileMode.Open);


// Mail
mail.Credentials(credentials)
    .EnableSsl()
    .From("mbarkdev@outlook.com")
    .To("mbarktiesto@outlook.com")
    .Subject("Global test N4")
    .Body("Hello dear from the global test N4")
    .Attachments(attachment)
    .Attachments(fileAsStream, "application/vnd.ms-excel")
    .Send();

Console.WriteLine("Sent");

#endregion

Console.ReadKey();