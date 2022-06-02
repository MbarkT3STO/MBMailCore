
using System.Net;
using System.Reflection;
using MBMailCore.Core;
using MBMailCore.Extensions;

var host = "smtp-mail.outlook.com";
var port = 587;

// Create mail object
var mail = new Mail( host , port );

// Credentials
var credentials = new NetworkCredential( "mbarkdev@outlook.com" , "X@123456@X" );

Console.WriteLine( "Sending..." );

// Mail
mail.Credentials( credentials )
    .From( "mbarkdev@outlook.com" )
    .To( "mbarktiesto@outlook.com" )
    .Subject( "Global test N1" )
    .Body( "Hello dear from the global test N1" )
    .Send();

Console.WriteLine("Sent");

Console.ReadKey();