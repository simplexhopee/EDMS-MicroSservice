using System.Runtime.Serialization;

namespace UserService.Shared.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : Exception
    {
      

        public EntityNotFoundException(string? message) : base(message)
        {
        }

      
    }
}