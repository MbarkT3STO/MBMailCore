namespace MBMailCore.Exceptions;

public class EmailIsNotValidException : Exception
{
    public EmailIsNotValidException(string mail) : base($"Email is not valid : {mail} " )
    {
        
    }
}