
using MBMailCore.Core;
using MBMailCore.Extensions;

var host = "smtp.outlook.com";
var port = 2525;

// Create mail object
var mail = new Mail( host , port );

// Set Username and Password
mail.SetUsername("MBARK").SetPassword("123456");

var privateUsername = mail.GetType().GetProperty( "Username" ).GetValue( mail ).ToString();
var privatePassword = mail.GetType().GetProperty( "Password" ).GetValue( mail ).ToString();

Console.WriteLine(privateUsername);
Console.WriteLine(privatePassword);