using System.Runtime.Serialization;

namespace UserService.Application.Commands.Login
{
    [Serializable]
   public class AuthenticationException : Exception
    {
       

        public AuthenticationException(string? message) : base(message)
        {
        }

   }
}