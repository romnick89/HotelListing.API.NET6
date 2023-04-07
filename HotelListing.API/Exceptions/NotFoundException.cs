namespace HotelListing.API.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key) : base($"{name} ({key}) Was Not Found")
        {

        }
    }
}
