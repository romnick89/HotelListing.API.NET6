namespace HotelListing.API.Core.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string name, object key, string errorName) : base($"{name} ({key}) Bad Request - " + errorName)
        {
        }
    }
}
