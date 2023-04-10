namespace HotelListing.API.Core.Models.Users
{
    public class AuthResponseModel
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
