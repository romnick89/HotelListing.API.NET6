using Serilog.Sinks.PeriodicBatching;

namespace HotelListing.API.Models
{
    public class QueryParameters
    {
        public int _pageSize = 15;
        public int StartIndex { get; set; }
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }
    }
}
