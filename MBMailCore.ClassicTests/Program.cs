
using System.Reflection;
using MBMailCore.Core;
using MBMailCore.Extensions;

var host = "smtp.outlook.com";
var port = 2525;

// Create mail object
var mail = new Mail( host , port );

// Set Username and Password
mail.SetUsername("MBARK").SetPassword("123456");

var privateUsername = typeof( Mail ).GetProperty("Username", BindingFlags.NonPublic | BindingFlags.Instance ).GetValue(mail, null).ToString();
var privatePassword = typeof(Mail).GetProperty( "Password" , BindingFlags.NonPublic | BindingFlags.Instance ).GetValue(mail, null).ToString();

Console.WriteLine(privateUsername);
Console.WriteLine(privatePassword);

Console.ReadKey();