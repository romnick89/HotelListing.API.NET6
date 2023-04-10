using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HotelListing.API.Core.Exceptions
{
    public class UnauthorisedException : ApplicationException
    {
        public UnauthorisedException(string name, object key) : base($"{name} ({key}) Unauthorised")
        {
        }
        public UnauthorisedException(string name) : base($"{name} Unauthorised")
        {
        }
    }
}
