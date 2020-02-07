using System;

namespace MovieShop.Core.Exceptions
{
    public class EmailExistsException: Exception
    {
        public EmailExistsException(string message): base(message)
        {
            
        }
    }
}