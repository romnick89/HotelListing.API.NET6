namespace HotelListing.API.Core.Models
{
    public class QueryPaged<T>
    {
        public int TotalCount { get; set; }
        public int PageNumber {get; set; }
        public int RecordNumber { get; set; }
        public List<T> Items { get; set; }
    }

}
